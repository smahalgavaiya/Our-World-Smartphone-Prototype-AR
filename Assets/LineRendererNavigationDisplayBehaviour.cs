using System;
using OurWorld.Scripts.Interfaces.MapAPI.Directions;
using UnityEngine;

namespace OurWorld.Scripts.Navigation.Directions
{
    public class LineRendererNavigationDisplayBehaviour : IDirectionsDisplayStrategy
    {
        private LineRenderer _lineRenderer;
        public bool Active => _lineRenderer && _lineRenderer.gameObject.activeInHierarchy;

        public void InitializeNavigationDisplay(WaypointStep[] steps,Vector3 startingPosition)
        {
            _lineRenderer = CreateLineRenderer();

            Vector3[] lineRendrerPoisitons = new Vector3[steps.Length + 1];

            lineRendrerPoisitons[0] = startingPosition;

            for (int i = 0; i < steps.Length; i++)
            {
                lineRendrerPoisitons[i + 1] = steps[i].UnityLocation;
            }

            _lineRenderer.positionCount = steps.Length;
            _lineRenderer.SetPositions(lineRendrerPoisitons);

        }

        public void DisposeActiveNavigationDisplay()
        {
            UnityEngine.Object.Destroy(_lineRenderer.gameObject);
            _lineRenderer = null;
        }

        public void StartNavigationDisplay()
        {
            _lineRenderer.gameObject.SetActive(true);
        }

        public void StopNavigationDisplay()
        {
            _lineRenderer.gameObject.SetActive(false);
        }

        public void UpdateDisplayedRoute(WaypointStep[] steps,Vector3 startingPosition)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {

        }

        #region Helpers
        private LineRenderer CreateLineRenderer()
        {
            GameObject lineRendererObject = new GameObject("DirectionsLineRenderer");

            LineRenderer lr = lineRendererObject.AddComponent<LineRenderer>();

            lr.useWorldSpace = true;

            lr.gameObject.SetActive(false);

            return lr;
        }

        #endregion
    }
}