using System;
using System.Collections.Generic;
using System.Linq;
using Mapbox.Unity.Map;
using OurWorld.Scripts.DataModels.GeolocationData;
using OurWorld.Scripts.Extensions;
using OurWorld.Scripts.Helpers;
using OurWorld.Scripts.Interfaces.MapAPI;
using UnityEngine;

namespace OurWorld.Scripts.Controllers
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

            _activeNavigation = new LineRendererNavigationStrategy();

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
    public class LineRendererNavigationStrategy : IDirectionsDisplayStrategy
    {
        private Geolocation[] _waypoints;
        private WaypointStep[] _steps;
        private LineRenderer _lineRenderer;
        private Func<Geolocation, Vector3> _locationSolver;

        public bool Active => _lineRenderer;

        public void StartNavigation(List<Geolocation> waypoints, Func<Geolocation, Vector3> locationSolver)
        {
            _locationSolver = locationSolver;

            _waypoints = waypoints.ToArray();

            CreateSteps(waypoints, ref _steps);

            _lineRenderer = CreateLineRenderer();

            Vector3[] lineRendrerPoisitons = new Vector3[_steps.Length + 1];

            lineRendrerPoisitons[0] = locationSolver(Geolocation.TempPlayerPosition);

            for (int i = 0; i < _steps.Length; i++)
            {
                lineRendrerPoisitons[i + 1] = locationSolver(_steps[i].Location);
            }

            _lineRenderer.positionCount = _steps.Length;
            _lineRenderer.SetPositions(lineRendrerPoisitons);

        }
        public void DisposeActiveNavigation()
        {
            UnityEngine.Object.Destroy(_lineRenderer.gameObject);
            _lineRenderer = null;
        }
        public void UpdateRoute(List<Geolocation> waypoints)
        {
            throw new System.NotImplementedException();
        }

        #region Helpers
        private void CreateSteps(List<Geolocation> waypoints, ref WaypointStep[] steps)
        {
            steps = new WaypointStep[waypoints.Count];

            Geolocation currentPosition = Geolocation.TempPlayerPosition;

            for (int i = 0; i < waypoints.Count; i++)
            {
                Geolocation waypoint = waypoints[i];
                float distance = currentPosition.DistanceTo(waypoint);
                steps[i] = new WaypointStep(waypoint, i, distance);
            }
        }
        private LineRenderer CreateLineRenderer()
        {
            GameObject lineRendererObject = new GameObject("DirectionsLineRendere");

            LineRenderer lr = lineRendererObject.AddComponent<LineRenderer>();
            
            lr.useWorldSpace = true;

            return lr;
        }
        #endregion
    }
    internal struct WaypointStep
    {
        public readonly Geolocation Location;
        public readonly int StepIndex;
        public float Distance;

        public WaypointStep(Geolocation location, int stepIndex, float distance)
        {
            Location = location;
            StepIndex = stepIndex;
            Distance = distance;
        }
        public WaypointStep(Geolocation location, int stepIndex) : this(location, stepIndex, -1)
        {

        }
    }
}