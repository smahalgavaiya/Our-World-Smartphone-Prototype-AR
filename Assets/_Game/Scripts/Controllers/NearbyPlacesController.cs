using OurWorld.Scripts.DataModels.GeolocationData;
using OurWorld.Scripts.DataModels.MapAPIRequests;
using OurWorld.Scripts.Interfaces;
using OurWorld.Scripts.Interfaces.MapAPI;
using OurWorld.Scripts.Views.ParksList;
using UnityEngine;
using OurWorld.Scripts.Extensions;
using OurWorld.Scripts.Interfaces.MapAPI.Geocoding;

namespace OurWorld.Scripts.Controllers
{
    public class NearbyPlacesController : MonoBehaviour
    {
        [SerializeField] private NearbyPlacesListElement _nearbyPlacesListElement;
        private IMapAPIProvider _mapApiProvider;

        public async void Initialize(IMapAPIProvider mapAPIProvider)
        {
            _mapApiProvider = mapAPIProvider;

            var playerPosition = Geolocation.TempPlayerPosition;

            IRequest request = new MapBoxForwardGeocodingRequest(playerPosition, playerPosition.GetBoundingBox(1f), new string[] { "poi" }, "park");

            var response = await _mapApiProvider.GeocodingProvider.POISearchAsync(request);

            _nearbyPlacesListElement.Initialize(response.Places, OnPlaceSelected);
        }

        private void OnPlaceSelected(IPointOfInterest placeData)
        {

        }
    }
}