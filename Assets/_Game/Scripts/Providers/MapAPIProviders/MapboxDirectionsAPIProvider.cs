using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using OurWorld.Scripts.Adapters;
using OurWorld.Scripts.DataModels.Enums;
using OurWorld.Scripts.DataModels.GeolocationData;
using OurWorld.Scripts.DataModels.MapAPIRequests;
using OurWorld.Scripts.Interfaces.MapAPI;

namespace OurWorld.Scripts.Providers.MapAPIProviders
{
    public class MapboxDirectionsAPIProvider : IDirectionsAPIProvider
    {
        private MapboxDirectionsAdapter _adapter;
        public MapboxDirectionsAPIProvider()
        {
            _adapter = new MapboxDirectionsAdapter();    
        }

        public async UniTask<List<Geolocation>> GetDirectionsAsync(Geolocation startPoint, Geolocation endPoint, RoutingType routingType = RoutingType.Walking)
        {
            DirectionsRequest request = new DirectionsRequest(new []{startPoint,endPoint},routingType);

            var response = await _adapter.GetDirectionsAsync(request);

            return response.Routes[0].Geometry;
        }
    }
}