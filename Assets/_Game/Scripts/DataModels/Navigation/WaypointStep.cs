using OurWorld.Scripts.DataModels.GeolocationData;
using UnityEngine;

namespace OurWorld.Scripts.Navigation.Directions
{
    public struct WaypointStep
    {
        public readonly Geolocation Location;
        public readonly Vector3 UnityLocation;
        public readonly int StepIndex;
        public float Distance;

        public WaypointStep(Geolocation location, Vector3 unityLocation, int stepIndex, float distance)
        {
            Location = location;
            StepIndex = stepIndex;
            Distance = distance;
            UnityLocation = unityLocation;
        }
        public WaypointStep(Geolocation location, Vector3 unityLocation, int stepIndex) : this(location, unityLocation, stepIndex, -1)
        {

        }
    }
}