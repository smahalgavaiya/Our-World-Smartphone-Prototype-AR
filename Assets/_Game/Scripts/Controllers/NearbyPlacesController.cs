using OurWorld.Scripts.DataModels.GeolocationData;
using OurWorld.Scripts.DataModels.MapAPIRequests;
using OurWorld.Scripts.Interfaces;
using OurWorld.Scripts.Interfaces.MapAPI;
using OurWorld.Scripts.Views.ParksList;
using UnityEngine;
using OurWorld.Scripts.Extensions;
using OurWorld.Scripts.Interfaces.MapAPI.Geocoding;
using Mapbox.Unity.Location;

namespace OurWorld.Scripts.Controllers
{
    public class NearbyPlacesController : MonoBehaviour
    {
        [SerializeField] private NearbyPlacesListElement _nearbyPlacesListElement;
        private IMapAPIProvider _mapApiProvider;

        public LocationProviderFactory locationProviderFactory;

        public void Initialize(IMapAPIProvider mapAPIProvider)
        {
            _mapApiProvider = mapAPIProvider;

             searchNewPlacesNearbyAndClearOld("park");
        }

        public async void searchNewPlacesNearbyAndClearOld(string placeSearchType)
        {
            Geolocation playerPosition = Geolocation.TempPlayerPosition;
            Debug.Log(playerPosition);
            AvatarInfoManager.Instance.currentplaceSearchType = placeSearchType;
            IRequest request = new MapBoxForwardGeocodingRequest(playerPosition, playerPosition.GetBoundingBox(100f), new string[] { "poi" }, placeSearchType);
            var response = await _mapApiProvider.GeocodingProvider.POISearchAsync(request);
            _nearbyPlacesListElement.Initialize(response.Places, OnPlaceSelected);

        }

        private void OnPlaceSelected(IPointOfInterest placeData)
        {

        }
    }
}