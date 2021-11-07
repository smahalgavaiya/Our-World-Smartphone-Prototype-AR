using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Mapbox.Unity.Map;
using OurWorld.Scripts.Behaviours.Directions.NavigationBehaviours;
using OurWorld.Scripts.DataModels.GeolocationData;
using OurWorld.Scripts.Extensions;
using OurWorld.Scripts.Interfaces.MapAPI;
using OurWorld.Scripts.Interfaces.MapAPI.Directions;
using UnityEngine;

namespace OurWorld.Scripts.Navigation.Directions
{
    public class DirectionsController : MonoBehaviour
    {
        [SerializeField] private float _updateInterval = 2f;
        [SerializeField] private Transform _avatar;
        private IDirectionsAPIProvider _directionsAPIProvider;
        private INavigationBehaviour _activeNavigation;

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

            if (Geolocation.TempPlayerPosition.DistanceTo(targetLocation) < 0.1f)
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

            _activeNavigation = new DefaultNavigationBehaviour(LocationSolver);

            _activeNavigation.StartNavigation(waypoints);

            StartCoroutine(UpdateCoroutine());
        }
        public void Dispose()
        {
            if (_activeNavigation == null) return;
            _activeNavigation.DisposeActiveNavigation();
            _activeNavigation = null;
        }

        private IEnumerator UpdateCoroutine()
        {
            var wait = new WaitForSeconds(_updateInterval);

            while(_activeNavigation != null && _activeNavigation.Active)
            {
                yield return wait;
                _activeNavigation.Update();
            }
        }

        private Vector3 LocationSolver(Geolocation location)
        {
            return new Vector3(0, 0.5f, 0) + _map.GeoToWorldPosition(new Mapbox.Utils.Vector2d(location.Latitude, location.Longitude));
        }

        private void OnDrawGizmos() {
            if(_activeNavigation == null) return;

            var defaultNav = _activeNavigation as DefaultNavigationBehaviour;
            Gizmos.DrawWireCube(defaultNav.StepBounds.center,defaultNav.StepBounds.size);
        }
    }
}