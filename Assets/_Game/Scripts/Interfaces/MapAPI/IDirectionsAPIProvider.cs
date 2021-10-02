using System.Collections.Generic;
using OurWorld.Scripts.DataModels.Enums;
using OurWorld.Scripts.DataModels.GeolocationData;

namespace OurWorld.Scripts.Interfaces.MapAPI
{
    public interface IDirectionsAPIProvider
    {
         List<Geolocation> GetDirections(Geolocation startPoint,Geolocation endPoint,RoutingType routingType = RoutingType.Walking);
    }
}