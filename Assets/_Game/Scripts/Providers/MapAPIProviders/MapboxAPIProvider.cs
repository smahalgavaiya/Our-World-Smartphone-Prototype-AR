using System;
using System.Linq;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Mapbox.Geocoding;
using Mapbox.Utils;
using OurWorld.Scripts.DataModels;
using OurWorld.Scripts.DataModels.GeolocationData;
using OurWorld.Scripts.Extensions;
using OurWorld.Scripts.Helpers;
using OurWorld.Scripts.Helpers.DataMappers;
using OurWorld.Scripts.Interfaces;
using OurWorld.Scripts.Interfaces.MapAPI;
using OurWorld.Scripts.Utilities.DataSerializers;

namespace OurWorld.Scripts.Providers.MapAPIProviders
{
    public class MapboxAPIProvider : IMapAPIProvidder
    {
        private const string _apiToken = "pk.eyJ1IjoiYmxhY3F2dmUiLCJhIjoiY2t0cTNlbDdsMHNueDJvcXUzNGFtMmI5aiJ9.d3FBlEYgLcTn-bfqV2uXrQ";
        private const string _parkSearchKeyword = "park";
        private readonly IWebRequestHelper _webRequestHelper;

        public MapboxAPIProvider()
        {
            _webRequestHelper = new WebRequestHelper(new MapBoxNewtonsoftJsonSerializerOption());
        }
        public async UniTask<List<ParkData>> GetNearbyParksAsync(Geolocation playerLocation, float radius)
        {
            var forwardGeocodeResource = new ForwardGeocodeResource(_parkSearchKeyword);

            forwardGeocodeResource.Types = new string[] { "poi" };

            var boundingBox = playerLocation.GetBoundingBox(radius);

            forwardGeocodeResource.Bbox = new Vector2dBounds(boundingBox.MinPoint, boundingBox.MaxPoint);

            forwardGeocodeResource.Proximity = playerLocation;

            var uriBuilder = new UriBuilder(forwardGeocodeResource.GetUrl());

            var tokenQueryParameter = $"access_token={_apiToken}";

            if (uriBuilder.Query != null && uriBuilder.Query.Length > 1)
                uriBuilder.Query = $"{uriBuilder.Query.Substring(1)}&{tokenQueryParameter}";
            else
                uriBuilder.Query = $"?{tokenQueryParameter}";

            var result = await _webRequestHelper.GetAsync<ForwardGeocodeResponse>(uriBuilder.Uri);

            IDataMapper<Feature, ParkData> parkDataMapper = new ParkDataMapper();

            List<ParkData> parkDataList = new List<ParkData>();

            if (!result.Success) return parkDataList;

            var parksList = ExtractParksFromGeocodingResponse(result.Data);

            foreach (var feature in parksList)
            {
                parkDataList.Add(parkDataMapper.MapObject(feature));
            }

            return parkDataList;
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
        #endregion


    }
}



