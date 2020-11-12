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

public class GetNearbyParks : MonoBehaviour
{
    string apiKey = "AIzaSyAw4JyCQ677K3hlOq94apInAK6To-OjUHs";
    LocationAPIManager locationAPIManager;
    [SerializeField]
    private Transform parentObject;
    [SerializeField]
    private GameObject buttonPrefab;
    [SerializeField]
    private GPSLocationProvider locationProvider;
    private void Awake()
    {
        locationAPIManager = new GoogleLocationAPIProvider(apiKey);
    }
    private async void Start()
    {
        if (!locationProvider)
        {
            Debug.LogError("Location provider is null");
            return;
        }

        var location = new GeoLocation
        {
            Lat = 39.996117,
            Lng = 32.707689
        };
        var places = await locationAPIManager.GetNearbyParks(new GoogleParkRequestSettings(1000, location));
        ListNearbyParks(places);

    }

    private void ListNearbyParks(List<Place> places)
    {
       foreach (var item in places)
       {
            ParkButton button = Instantiate(buttonPrefab,parentObject).GetComponent<ParkButton>();
            var place=new ParkButtonModel(item.Name,item.PlaceID,item.Location.ConvertToWrldLatLong());
            button.Setup(place);
       }
    }
}
