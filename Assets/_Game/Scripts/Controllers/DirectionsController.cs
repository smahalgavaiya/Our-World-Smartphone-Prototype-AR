using System.Collections.Generic;
using Mapbox.Unity.Map;
using OurWorld.Scripts.DataModels.GeolocationData;
using OurWorld.Scripts.Extensions;
using OurWorld.Scripts.Interfaces.MapAPI;
using UnityEngine;

namespace OurWorld.Scripts.Navigation.Directions
{
    public class DirectionsController : MonoBehaviour
    {
        private IDirectionsAPIProvider _directionsAPIProvider;
        private IDirectionsDisplayStrategy _activeNavigation;

        private AbstractMap _map;
        public bool IsNavigating => _activeNavigation != null && _activeNavigation.Active;

        public void Initialize(IDirectionsAPIProvider directionsAPIProvider)
        {
            _directionsAPIProvider = directionsAPIProvider;
            _map = FindObjectOfType<AbstractMap>();
        }
        public async void CreateDirectionsForTarget(Geolocation targetLocation)
        {
            if (IsNavigating)
            {
                Debug.Log("There is an active navigation, if you want do start new navigation you should first dispose active one");
                return;
            }

            if (Geolocation.TempPlayerPosition.DistanceTo(targetLocation) < 0.1)
            {
                Debug.Log("Target distance is lower than 100 meters");
                return;
            }

            List<Geolocation> waypoints = await _directionsAPIProvider.GetDirectionsAsync(Geolocation.TempPlayerPosition, targetLocation);

            if (waypoints == null || waypoints.Count < 1)
            {
                Debug.LogError("Waypoints can't be empty, please check your location query");
                return;
            }

            _activeNavigation = new LineRendererNavigationBehaviour();

            _activeNavigation.StartNavigation(waypoints, LocationSolver);
        }
        public void Dispose()
        {
            if (_activeNavigation == null) return;
            _activeNavigation.DisposeActiveNavigation();
            _activeNavigation = null;
        }
        private Vector3 LocationSolver(Geolocation location)
        {
            return new Vector3(0,0.5f,0) + _map.GeoToWorldPosition(new Mapbox.Utils.Vector2d(location.Latitude,location.Longitude));
        }
    }
}