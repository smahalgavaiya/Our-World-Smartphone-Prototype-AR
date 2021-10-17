using OurWorld.Scripts.DataModels.GeolocationData;
using OurWorld.Scripts.Interfaces.MapAPI;
using UnityEngine;

namespace OurWorld.Scripts.Controllers
{
    public class DirectionsController : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;

        [SerializeField] private IDirectionsAPIProvider _directionsAPIProvider;

        public bool IsNavigating { get; private set; } = false;

        public void Initialize(IDirectionsAPIProvider directionsAPIProvider)
        {
            _directionsAPIProvider = directionsAPIProvider;
        }
        public void CreateDirectionsForTarget(Geolocation targetLocation)
        {
            if (IsNavigating)
            {
                Debug.Log("There is an active navigation, if you want do start new navigation you should first dispose active one");
                return;
            }

        }
    }
}