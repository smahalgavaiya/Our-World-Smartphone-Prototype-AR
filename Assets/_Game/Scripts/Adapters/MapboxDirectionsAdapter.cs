using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Mapbox.Directions;
using Mapbox.Platform;
using Mapbox.Unity;
using Mapbox.Utils;
using OurWorld.Scripts.DataModels.Enums;
using OurWorld.Scripts.DataModels.MapAPIRequests;
using OurWorld.Scripts.Helpers;
using OurWorld.Scripts.Helpers.DataMappers;
using OurWorld.Scripts.Interfaces;
using DirectionsResponse = OurWorld.Scripts.DataModels.MapAPIResponses.DirectionsResponseModels.DirectionsResponse;
using MBDirectionsResponse = Mapbox.Directions.DirectionsResponse;


namespace OurWorld.Scripts.Adapters
{
    public class MapboxDirectionsAdapter
    {
        private IDataMapper<MBDirectionsResponse, DirectionsResponse> _directionsResponseMapper;

        public MapboxDirectionsAdapter()
        {
            _directionsResponseMapper = new MapboxDirectionsResponseMapper();
        }

        public async UniTask<DirectionsResponse> GetDirectionsAsync(DirectionsRequest request, RoutingType routingType = RoutingType.Walking)
        {
            Directions directions = MapboxAccess.Instance.Directions;

            DirectionResource resource = CreateResourceFromRequest(request);

            IAsyncRequest handle = directions.Query(resource, OnResponse);

            MBDirectionsResponse mbDirectionsResponse = null;

            await UniTask.WaitUntil(() => handle.IsCompleted && mbDirectionsResponse != null);

            DirectionsResponse directionsResponse = _directionsResponseMapper.MapObject(mbDirectionsResponse);

            return directionsResponse;

            void OnResponse(MBDirectionsResponse response)
            {
                mbDirectionsResponse = response;
            }
        }
        private DirectionResource CreateResourceFromRequest(DirectionsRequest request)
        {
            Vector2d[] coordinates = request.Coordinates.Select(x => LocationTypeConverter.GeolocationToVector2d(x, false)).ToArray();

            RoutingProfile routingProfile = GetRoutingProfileFromEnum(request.RoutingType);

            return new DirectionResource(coordinates, routingProfile){
                Alternatives = request.AlternateRoutes
            };
        }
        private RoutingProfile GetRoutingProfileFromEnum(RoutingType routingType)
        {
            switch (routingType)
            {
                case RoutingType.Walking:
                    return RoutingProfile.Walking;
                case RoutingType.Driving:
                    return RoutingProfile.Driving;
                default:
                    return RoutingProfile.Walking;
            }

        }
    }



}