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
        public readonly static string ApiEndpoint = "geocoding/v5/";
        public readonly static string Mode = "mapbox.places/";

        public readonly Geolocation Proximity;

        public readonly BoundingBox BoundingBox;

        public readonly string[] SearchTypes;

        public readonly string Query;

        public MapBoxForwardGeocodingRequest(Geolocation proximity, BoundingBox boundingBox, string[] searchTypes, string query)
        {
            Proximity = proximity;
            BoundingBox = boundingBox;
            SearchTypes = searchTypes;
            Query = query;
        }
        public virtual string GetRequestURLParameters()
        {
            Dictionary<string, string> options = new Dictionary<string, string>();

            options.Add("bbox", new Vector2dBounds(
                LocationTypeConverter.GeolocationToVector2d(BoundingBox.MinPoint),
                LocationTypeConverter.GeolocationToVector2d(BoundingBox.MaxPoint)
            ).ToString());

            options.Add("proximity", LocationTypeConverter.GeolocationToVector2d(Proximity).ToString());

            options.Add("types", string.Join(",", SearchTypes));

            return $"{ApiEndpoint}{Mode}{Uri.EscapeUriString(Query)}.json{WebRequestHelper.EncodeQueryStringParameters(options)}";
        }
    }
}