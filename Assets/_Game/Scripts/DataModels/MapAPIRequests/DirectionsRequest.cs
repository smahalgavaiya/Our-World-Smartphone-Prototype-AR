using OurWorld.Scripts.DataModels.Enums;
using OurWorld.Scripts.DataModels.GeolocationData;

namespace OurWorld.Scripts.DataModels.MapAPIRequests
{
    public class DirectionsRequest
    {
        public readonly Geolocation[] Coordinates;
        public readonly RoutingType RoutingType;
        public readonly bool? AlternateRoutes;

        public DirectionsRequest(Geolocation[] coordinates, RoutingType routingType, bool? alternateRoutes = null)
        {
            Coordinates = coordinates;
            RoutingType = routingType;
            AlternateRoutes = alternateRoutes;
        }
    }

}