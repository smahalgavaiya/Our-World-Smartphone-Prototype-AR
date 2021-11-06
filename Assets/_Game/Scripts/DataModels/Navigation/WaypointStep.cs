using OurWorld.Scripts.DataModels.GeolocationData;

namespace OurWorld.Scripts.Navigation.Directions
{
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