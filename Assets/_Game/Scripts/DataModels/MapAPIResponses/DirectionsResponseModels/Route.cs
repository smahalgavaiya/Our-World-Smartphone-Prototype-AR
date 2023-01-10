using System.Collections.Generic;
using OurWorld.Scripts.DataModels.GeolocationData;

namespace OurWorld.Scripts.DataModels.MapAPIResponses.DirectionsResponseModels
{
    public class Route
    {
        public readonly List<Geolocation> Geometry;

        public readonly double DurationInSecond;

        public readonly double DistanceInMeters;

        public Route(List<Geolocation> geometry, double durationInSecond, double distanceInMeters)
        {
            Geometry = geometry;
            DurationInSecond = durationInSecond;
            DistanceInMeters = distanceInMeters;
        }
    }
}