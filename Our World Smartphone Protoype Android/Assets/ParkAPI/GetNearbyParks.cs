using UnityEngine;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System.Linq;
using System.Collections.Generic;
using ParkAPI;
using ParkAPI.Objects;
using ParkAPI.Settings;
using ParkFindingScripts.Models;
using Wrld.Space;
using Wrld;
using System;
using System.Threading.Tasks;

public class GetNearbyParks : MonoBehaviour
{
    [SerializeField]
    string apiKey = "";
    LocationAPIManager locationAPIManager;
    [SerializeField]
    private Transform parentObject;
    [SerializeField]
    private GameObject buttonPrefab;
    [SerializeField]
    private GPSLocationProvider locationProvider;
    [SerializeField]
    private int radius = 1000;
    [SerializeField]
    private GeographicTransform avatarLocationProvider;
    GeoLocation location;
    private GoogleParkRequestSettings googleRequestSettings;
    

    private void OnEnable() => locationProvider.OnLocationUpdated += LocationUpdated;
    private void OnDisable() => locationProvider.OnLocationUpdated -= LocationUpdated;
    private async void LocationUpdated(LatLong obj)
    {
        location.Lat = obj.GetLatitude();
        location.Lng = obj.GetLongitude();
        await GetParks(location);
    }

    private void Awake()
    {
        locationAPIManager = new GoogleLocationAPIProvider(apiKey);
        googleRequestSettings = new GoogleParkRequestSettings(radius, null);
    }
    private async void Start()
    {
        if (!locationProvider){
            Debug.LogError("Location provider is null");
            return;
        }
        if(!avatarLocationProvider){
            Debug.LogError("Avatar location provider is null");
            return;
        }
        LatLong currentLocation = avatarLocationProvider.GetPosition();
        location.Lat=currentLocation.GetLatitude();
        location.Lng=currentLocation.GetLongitude();
        await GetParks(location);
    }
    private async Task GetParks(GeoLocation location)
    {
        googleRequestSettings.Location = location;
        var places = await locationAPIManager.GetNearbyParks(googleRequestSettings);
        ListNearbyParks(places);
    }
    private void ListNearbyParks(List<Place> places)
    {
        foreach (var item in places)
        {
            ParkButton button = Instantiate(buttonPrefab, parentObject).GetComponent<ParkButton>();
            var place = new ParkButtonModel(item.Name, item.PlaceID, item.Location.ConvertToWrldLatLong());
            button.Setup(place);
        }
    }
}
