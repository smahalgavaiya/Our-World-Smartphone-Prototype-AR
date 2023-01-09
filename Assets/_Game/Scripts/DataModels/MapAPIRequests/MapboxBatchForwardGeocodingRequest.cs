using System;
using System.Collections.Generic;
using Mapbox.Utils;
using OurWorld.Scripts.DataModels.GeolocationData;
using OurWorld.Scripts.Helpers;
using OurWorld.Scripts.Interfaces;

namespace OurWorld.Scripts.DataModels.MapAPIRequests
{
    public class MapboxBatchForwardGeocodingRequest : MapBoxForwardGeocodingRequest, IBatchRequest
    {
        public readonly string[] Queries;
        private readonly string _encodedOptions;
        private int iteration = 0;
        public MapboxBatchForwardGeocodingRequest(Geolocation proximity, BoundingBox boundingBox, string[] searchTypes, string[] queries) : base(proximity, boundingBox, searchTypes, queries[0])
        {
            Queries = queries;
            _encodedOptions = WebRequestHelper.EncodeQueryStringParameters(PopulateCommonOptions());
        }
        public IEnumerable<string> GetRequestMultipleURLParameters()
        {
            foreach (string query in Queries){
                yield return $"{ApiEndpoint}{Mode}{Uri.EscapeUriString(query)}.json{_encodedOptions}";
            }
        }
        private Dictionary<string, string> PopulateCommonOptions()
        {
            Dictionary<string, string> options = new Dictionary<string, string>();

            options.Add("bbox", new Vector2dBounds(
                LocationTypeConverter.GeolocationToVector2d(BoundingBox.MinPoint),
                LocationTypeConverter.GeolocationToVector2d(BoundingBox.MaxPoint)
            ).ToString());

            options.Add("proximity", LocationTypeConverter.GeolocationToVector2d(Proximity).ToString());

            options.Add("types", string.Join(",", SearchTypes));

            return options;
        }
    }
}