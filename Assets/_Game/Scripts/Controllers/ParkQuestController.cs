using OurWorld.Scripts.DataModels;
using OurWorld.Scripts.DataModels.GeolocationData;
using OurWorld.Scripts.Interfaces.MapAPI;
using OurWorld.Scripts.Views.ParksList;
using UnityEngine;

namespace OurWorld.Scripts.Controllers
{
    public class ParkQuestController : MonoBehaviour
    {
        [SerializeField] private ParkListElement _parkListElement;
        private IMapAPIProvider _mapApiProvider;

        public async void Initialize(IMapAPIProvider mapAPIProvider)
        {
            _mapApiProvider = mapAPIProvider;

            var nearbyParks = await _mapApiProvider.GetNearbyParksAsync(Geolocation.TempPlayerPosition, 1f);

            _parkListElement.Initialize(nearbyParks,OnParkSelected);
        }

        private void OnParkSelected(ParkData parkData)
        {

        }
    }
}