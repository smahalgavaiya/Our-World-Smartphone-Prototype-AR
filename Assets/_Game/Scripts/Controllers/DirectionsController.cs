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
using UnityEngine.UI;

namespace OurWorld.Scripts.Navigation.Directions
{
    public class DirectionsController : MonoBehaviour
    {
        public Text cyclingTime, drivingTime;
        [SerializeField] private float _updateInterval = 2f;
        [SerializeField] private Transform _avatar;
        private IDirectionsAPIProvider _directionsAPIProvider;
        private INavigationBehaviour _activeNavigation;

        private AbstractMap _map;
        public bool IsNavigating => _activeNavigation != null && _activeNavigation.Active;

        private Geolocation _targetLocation;

        public void Initialize(IDirectionsAPIProvider directionsAPIProvider)
        {
            _directionsAPIProvider = directionsAPIProvider;
            _map = FindObjectOfType<AbstractMap>();
        }
        public async void CreateDirectionsForTarget(Geolocation targetLocation)
        {
            _targetLocation = targetLocation;
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
                if (_activeNavigation != null)
                {
                    _activeNavigation.Update();
                    if (AvatarInfoManager.Instance.currentplaceSearchType == "Park")
                        CheckIfTargetReachedThenGiveAwards();
                }
            }
        }

        void CheckIfTargetReachedThenGiveAwards()
        {
            StartCoroutine(onStayingNaturePlacesReward());
            if (Geolocation.TempPlayerPosition.DistanceTo(_targetLocation) < 0.1f)
            {
                //Check for 
                Debug.Log("Target distance is lower than 100 meters Reached");
                //6b
                //check if location is already on shared pref
                if (AvatarInfoManager.Instance.isThisLocationOnLocationVisitedCollection(_targetLocation.ToString()))
                {
                    //Give 100 karma new location
                    AvatarInfoManager.Instance.AddKarma("TargetReached", "Game", "TargetReached100", _targetLocation.ToString());
                    AvatarInfoManager.Instance.AddLocationToVistied(_targetLocation.ToString());
                }
                else
                { //give 50 karma old location 
                    AvatarInfoManager.Instance.AddKarma("TargetReached", "Game", "TargetReached50", _targetLocation.ToString());
                }

                StopCoroutine(UpdateCoroutine());
            }
        }
        private IEnumerator onStayingNaturePlacesReward()
        {
            var wait = new WaitForSeconds(60f);

            while (Geolocation.TempPlayerPosition.DistanceTo(_targetLocation) < 0.1f)
            {
                yield return wait;
                AvatarInfoManager.Instance.AddKarma("TargetStaying", "Game", "TargetStaying1", _targetLocation.ToString());
            }
            StopCoroutine(onStayingNaturePlacesReward());
        }

        private Vector3 LocationSolver(Geolocation location)
        {
            return new Vector3(0, 0.5f, 0) + _map.GeoToWorldPosition(new Mapbox.Utils.Vector2d(location.Latitude, location.Longitude));
        }

        private void OnDrawGizmos() {
            if(_activeNavigation == null) return;

            DefaultNavigationBehaviour defaultNav = _activeNavigation as DefaultNavigationBehaviour;
           // Gizmos.DrawWireCube(defaultNav.StepBounds.center,defaultNav.StepBounds.size);
        }
    }
}