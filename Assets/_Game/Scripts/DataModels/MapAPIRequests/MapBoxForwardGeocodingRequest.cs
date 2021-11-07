using System;
using System.Collections.Generic;
using Mapbox.Utils;
using OurWorld.Scripts.DataModels.GeolocationData;
using OurWorld.Scripts.Helpers;
using OurWorld.Scripts.Interfaces;

namespace OurWorld.Scripts.DataModels.MapAPIRequests
{
    public class MapBoxForwardGeocodingRequest : IRequest
    {
        public readonly Geolocation Proximity;

        public readonly BoundingBox BoundingBox;

        public readonly string[] SearchTypes;

        public readonly string Query;

        private readonly string _apiEndpoint = "geocoding/v5/";

		private readonly string _mode = "mapbox.places/";

        public MapBoxForwardGeocodingRequest(Geolocation proximity, BoundingBox boundingBox, string[] searchTypes, string query)
        {
            Proximity = proximity;
            BoundingBox = boundingBox;
            SearchTypes = searchTypes;
            Query = query;
        }

        public string GetRequestURLParameters()
        {
            Dictionary<string,string> options = new Dictionary<string, string>();

            options.Add("bbox",new Vector2dBounds(
                LocationTypeConverter.GeolocationToVector2d(BoundingBox.MinPoint),
                LocationTypeConverter.GeolocationToVector2d(BoundingBox.MaxPoint)
            ).ToString());

            options.Add("proximity",LocationTypeConverter.GeolocationToVector2d(Proximity).ToString());

            options.Add("types",string.Join(",",SearchTypes));

            return $"{_apiEndpoint}{_mode}{Uri.EscapeUriString(Query)}.json{WebRequestHelper.EncodeQueryStringParameters(options)}"; 
        }
    }
}