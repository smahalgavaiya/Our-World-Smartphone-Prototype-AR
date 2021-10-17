using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using OurWorld.Scripts.DataModels.Enums;
using OurWorld.Scripts.DataModels.GeolocationData;

namespace OurWorld.Scripts.Interfaces.MapAPI
{
    public interface IDirectionsAPIProvider
    {
         UniTask<List<Geolocation>> GetDirectionsAsync(Geolocation startPoint,Geolocation endPoint,RoutingType routingType = RoutingType.Walking);
    }
}