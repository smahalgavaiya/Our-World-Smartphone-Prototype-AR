using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using OurWorld.Scripts.DataModels;
using OurWorld.Scripts.DataModels.GeolocationData;
using OurWorld.Scripts.Extensions;
using OurWorld.Scripts.Helpers;
using OurWorld.Scripts.Interfaces;
using OurWorld.Scripts.Interfaces.MapAPI;
using OurWorld.Scripts.Utilities.DataSerializers;

public class MapboxAPIProvider : IMapAPIProvidder
{
    private const string _apiToken = "pk.eyJ1IjoiYmxhY3F2dmUiLCJhIjoiY2t0cTNlbDdsMHNueDJvcXUzNGFtMmI5aiJ9.d3FBlEYgLcTn-bfqV2uXrQ";
    private const string _baseUrl = "https://api.mapbox.com/geocoding/v5/";

    private const string _parkSearchKeyword = "park";
    private readonly IWebRequestHelper _webRequestHelper;

    private readonly Uri _baseUri;

    public MapboxAPIProvider()
    {
        _webRequestHelper = new WebRequestHelper(new NewtonsoftJsonSerializerOption());
        _baseUri = new Uri(_baseUrl);
    }
    public async UniTask<List<ParkData>> GetNearbyParksAsync(Geolocation playerLocation,float radius)
    {
        var parksUri = new Uri(_baseUri,$"mapbox.places/{_parkSearchKeyword}.json");

        var uriBuildder = new UriBuilder(parksUri);

        var queryParams = new Dictionary<string,string>{
            {"access_token",Uri.EscapeDataString(_apiToken)},
            {"types",Uri.EscapeDataString("poi")},
            {"proximity",Uri.EscapeDataString(playerLocation.ToString())},
            {"bbox",Uri.EscapeDataString(playerLocation.GetBoundingBox(radius).ToString())}
        };

        foreach (var kvp in queryParams)
        {
            if(uriBuildder.Query != null && uriBuildder.Query.Length > 1)
                uriBuildder.Query = uriBuildder.Query.Substring(1) + "&" + $"{kvp.Key}={kvp.Value}";
            else
                uriBuildder.Query = $"{kvp.Key}={kvp.Value}";
        }

        parksUri = uriBuildder.Uri;

        Debug.Log(parksUri.AbsoluteUri);
        Debug.Log(parksUri.AbsolutePath);

        var result = await _webRequestHelper.GetAsync<MapboxSearchResult>(parksUri);

        return null;
    }
}
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

    public class MapboxSearchResult
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("query")]
        public List<string> Query { get; set; }

        [JsonProperty("features")]
        public List<Feature> Features { get; set; }
    }


