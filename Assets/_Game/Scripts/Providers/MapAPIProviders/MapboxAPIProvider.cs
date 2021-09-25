using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using OurWorld.Scripts.DataModels;
using OurWorld.Scripts.DataModels.GeolocationData;
using OurWorld.Scripts.DataModels.MapAPIResponses;
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

        var result = await _webRequestHelper.GetAsync<MapboxSearchResponse>(parksUri);
        
        return null;
    }
}



