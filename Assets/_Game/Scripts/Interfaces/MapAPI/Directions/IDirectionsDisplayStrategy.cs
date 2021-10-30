using OurWorld.Scripts.Navigation.Directions;
using UnityEngine;

namespace OurWorld.Scripts.Interfaces.MapAPI.Directions
{
    public interface IDirectionsDisplayStrategy
    {
        public bool Active { get; }

        public void InitializeNavigationDisplay(WaypointStep[] steps,Vector3 startingPosition);

        public void StartNavigationDisplay();

        public void StopNavigationDisplay();

        public void DisposeActiveNavigationDisplay();

        public void UpdateDisplayedRoute(WaypointStep[] steps, Vector3 startingPosition);

        public void Update();
    }
}