using System.Collections.Generic;
using Newtonsoft.Json;

namespace OurWorld.Scripts.DataModels.MapAPIResponses
{
    public class MapboxSearchResponse
    {

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("query")]
        public List<string> Query { get; set; }

        [JsonProperty("features")]
        public List<Feature> Features { get; set; }


        public class Properties
        {
            [JsonProperty("foursquare")]
            public string Foursquare { get; set; }

            [JsonProperty("landmark")]
            public bool Landmark { get; set; }

            [JsonProperty("address")]
            public string Address { get; set; }

            [JsonProperty("category")]
            public string Category { get; set; }
        }

        public class Geometry
        {
            [JsonProperty("coordinates")]
            public List<double> Coordinates { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }
        }

        public class Feature
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("place_type")]
            public List<string> PlaceType { get; set; }

            [JsonProperty("relevance")]
            public int Relevance { get; set; }

            [JsonProperty("properties")]
            public Properties Properties { get; set; }

            [JsonProperty("text")]
            public string Text { get; set; }

            [JsonProperty("place_name")]
            public string PlaceName { get; set; }

            [JsonProperty("center")]
            public List<double> Center { get; set; }

            [JsonProperty("geometry")]
            public Geometry Geometry { get; set; }
        }
    }

}