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

            var nearbyParks = await _mapApiProvider.GetNearbyParksAsync(new Geolocation(32.707270, 39.995767), 1f);

            _parkListElement.Initialize(nearbyParks);
        }
    }
}