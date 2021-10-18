using System;
using System.Collections.Generic;
using OurWorld.Scripts.DataModels.GeolocationData;
using OurWorld.Scripts.Extensions;
using OurWorld.Scripts.Interfaces.MapAPI;
using UnityEngine;

namespace OurWorld.Scripts.Navigation.Directions
{
    public class LineRendererNavigationBehaviour : IDirectionsDisplayStrategy
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
                if (i == 0)
                {
                    float distance = currentPosition.DistanceTo(waypoint);
                     steps[i] = new WaypointStep(waypoint, i, distance);
                }
                steps[i] = new WaypointStep(waypoint, i);
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
}