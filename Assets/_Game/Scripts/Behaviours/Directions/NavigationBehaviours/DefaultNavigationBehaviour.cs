using System;
using System.Collections.Generic;
using OurWorld.Scripts.DataModels.GeolocationData;
using OurWorld.Scripts.Extensions;
using OurWorld.Scripts.Interfaces.MapAPI.Directions;
using OurWorld.Scripts.Navigation.Directions;
using UnityEngine;

namespace OurWorld.Scripts.Behaviours.Directions.NavigationBehaviours
{
    public class DefaultNavigationBehaviour : INavigationBehaviour
    {
        private Geolocation[] _waypoints;
        private WaypointStep[] _steps;
        private WaypointStep[] _completedSteps;
        private WaypointStep _currentStep;
        private Func<Geolocation, Vector3> _locationSolver;
        private IDirectionsDisplayStrategy _directionsDisplayStrategy;

        private bool _isNavigationActive;
        public bool Active => _isNavigationActive;

        public DefaultNavigationBehaviour(Func<Geolocation, Vector3> locationSolver)
        {
            _locationSolver = locationSolver;
            _directionsDisplayStrategy = new LineRendererNavigationDisplayBehaviour();
        }

        public void DisposeActiveNavigation()
        {
            _isNavigationActive = false;
            _directionsDisplayStrategy.DisposeActiveNavigationDisplay();
        }

        public void StartNavigation(List<Geolocation> waypoints)
        {
            _waypoints = waypoints.ToArray();

            CreateSteps();

            Vector3 startingPosition = _locationSolver(Geolocation.TempPlayerPosition);

            _directionsDisplayStrategy.InitializeNavigationDisplay(_steps, startingPosition);

            _directionsDisplayStrategy.StartNavigationDisplay();

            _isNavigationActive = true;
        }

        public void Update()
        {
            
        }

        public void UpdateRoute(List<Geolocation> waypoints)
        {
            _directionsDisplayStrategy.DisposeActiveNavigationDisplay();

            StartNavigation(waypoints);
        }

        #region Helpers
        private void CreateSteps()
        {
            _steps = new WaypointStep[_waypoints.Length];

            Geolocation currentPosition = Geolocation.TempPlayerPosition;

            for (int i = 0; i < _waypoints.Length; i++)
            {
                Geolocation waypoint = _waypoints[i];
                Vector3 unityLocation = _locationSolver(waypoint);
                if (i == 0)
                {
                    float distance = currentPosition.DistanceTo(waypoint);
                    _steps[i] = new WaypointStep(waypoint, unityLocation, i, distance);
                }
                _steps[i] = new WaypointStep(waypoint, unityLocation, i);
            }
        }

        #endregion
    }
}