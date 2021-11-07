using OurWorld.Scripts.DataModels;
using OurWorld.Scripts.DataModels.GeolocationData;
using OurWorld.Scripts.DataModels.MapAPIRequests;
using OurWorld.Scripts.Interfaces;
using OurWorld.Scripts.Interfaces.MapAPI;
using OurWorld.Scripts.Views.ParksList;
using UnityEngine;
using OurWorld.Scripts.Extensions;

namespace OurWorld.Scripts.Controllers
{
    public class ParkQuestController : MonoBehaviour
    {
        [SerializeField] private ParkListElement _parkListElement;
        private IMapAPIProvider _mapApiProvider;

        public async void Initialize(IMapAPIProvider mapAPIProvider)
        {
            _mapApiProvider = mapAPIProvider;

            var playerPosition = Geolocation.TempPlayerPosition;

            IRequest request = new MapBoxForwardGeocodingRequest(playerPosition, playerPosition.GetBoundingBox(1f), new string[] { "poi" }, "park");

            var response = await _mapApiProvider.GeocodingProvider.POISearchAsync(request);

            _parkListElement.Initialize(response.Places, OnParkSelected);
        }

        private void OnParkSelected(POIData parkData)
        {

        }
    }
}