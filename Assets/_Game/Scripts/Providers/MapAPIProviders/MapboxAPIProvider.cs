using System;
using System.Linq;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Mapbox.Geocoding;
using Mapbox.Utils;
using OurWorld.Scripts.DataModels;
using OurWorld.Scripts.DataModels.GeolocationData;
using OurWorld.Scripts.Helpers;
using OurWorld.Scripts.Helpers.DataMappers;
using OurWorld.Scripts.Interfaces;
using OurWorld.Scripts.Interfaces.MapAPI;
using OurWorld.Scripts.Utilities.DataSerializers;
using Mapbox.CheapRulerCs;
using OurWorld.Scripts.Extensions;

namespace OurWorld.Scripts.Providers.MapAPIProviders
{
    public class MapboxAPIProvider : IMapAPIProvider
    {
        public const string ApiToken = "pk.eyJ1IjoiYmxhY3F2dmUiLCJhIjoiY2t0cTNlbDdsMHNueDJvcXUzNGFtMmI5aiJ9.d3FBlEYgLcTn-bfqV2uXrQ";
        
        private const string _parkSearchKeyword = "park";
        private readonly IWebRequestHelper _webRequestHelper;
        private readonly IDirectionsAPIProvider _directionsAPIProvider;

        public IDirectionsAPIProvider DirectionsAPI => _directionsAPIProvider;
        public MapboxAPIProvider()
        {
            _webRequestHelper = new WebRequestHelper(new MapBoxNewtonsoftJsonSerializerOption());
            _directionsAPIProvider = new MapboxDirectionsAPIProvider();
        }
        public async UniTask<List<ParkData>> GetNearbyParksAsync(Geolocation playerLocation, float radius)
        {
            var forwardGeocodeResource = new ForwardGeocodeResource(_parkSearchKeyword);

            forwardGeocodeResource.Types = new string[] { "poi" };

            var boundingBox = playerLocation.GetBoundingBox(radius);

            forwardGeocodeResource.Bbox = new Vector2dBounds(
                LocationTypeConverter.GeolocationToVector2d(boundingBox.MinPoint),
                LocationTypeConverter.GeolocationToVector2d(boundingBox.MaxPoint)
                 );

            forwardGeocodeResource.Proximity = LocationTypeConverter.GeolocationToVector2d(playerLocation);

            var uriBuilder = new UriBuilder(forwardGeocodeResource.GetUrl());

            var tokenQueryParameter = $"access_token={ApiToken}";

            if (uriBuilder.Query != null && uriBuilder.Query.Length > 1)
                uriBuilder.Query = $"{uriBuilder.Query.Substring(1)}&{tokenQueryParameter}";
            else
                uriBuilder.Query = $"?{tokenQueryParameter}";

            var result = await _webRequestHelper.GetAsync<ForwardGeocodeResponse>(uriBuilder.Uri);

            List<ParkData> parkDataList = new List<ParkData>();

            if (!result.Success) return parkDataList;

            var parksList = ExtractParksFromGeocodingResponse(result.Data);

            return PopulateParkDataFromFeatures(parksList, playerLocation);
        }


        #region Helpers
        private List<Feature> ExtractParksFromGeocodingResponse(ForwardGeocodeResponse response)
        {
            return response.Features.Where(x =>
            {
                if (x.Properties == null) return false;

                if (x.Properties.TryGetValue("category", out object value))
                {
                    var categories = ((string)value).Split(',');

                    return categories.Contains(_parkSearchKeyword);
                }
                return false;
            }).ToList();
        }

        private List<ParkData> PopulateParkDataFromFeatures(List<Feature> features, Geolocation playerLocation)
        {
            IDataMapper<Feature, ParkData> parkDataMapper = new ParkDataMapper();

            var ruler = new CheapRuler(playerLocation.Latitude, CheapRulerUnits.Kilometers);

            List<ParkData> parkDataList = new List<ParkData>();

            foreach (var feature in features)
            {
                var parkData = parkDataMapper.MapObject(feature);
                parkData.Distance = (float)ruler.Distance(playerLocation.ToLonLatArray(), parkData.Geolocation.ToLonLatArray());
                parkDataList.Add(parkData);
            }

            return parkDataList;
        }
        #endregion


    }
}



