using MBDirectionsResponse = Mapbox.Directions.DirectionsResponse;
using MBRoute = Mapbox.Directions.Route;
using MBWaypoint = Mapbox.Directions.Waypoint;
using DirectionsResponse = OurWorld.Scripts.DataModels.MapAPIResponses.DirectionsResponseModels.DirectionsResponse;
using Route = OurWorld.Scripts.DataModels.MapAPIResponses.DirectionsResponseModels.Route;
using Waypoint = OurWorld.Scripts.DataModels.MapAPIResponses.DirectionsResponseModels.Waypoint;
using OurWorld.Scripts.Interfaces;
using OurWorld.Scripts.DataModels.MapAPIResponses.DirectionsResponseModels;
using System.Collections.Generic;
using OurWorld.Scripts.DataModels.GeolocationData;
using Mapbox.Utils;
using System.Linq;

namespace OurWorld.Scripts.Helpers.DataMappers
{
    public class MapboxDirectionsResponseMapper : IDataMapper<MBDirectionsResponse, DirectionsResponse>
    {
        public DirectionsResponse MapObject(MBDirectionsResponse sourceObject)
        {
            List<MBRoute> mbRoutes = sourceObject.Routes;
            List<MBWaypoint> mbWaypoints = sourceObject.Waypoints;

            List<Route> routes = new List<Route>();
            List<Waypoint> waypoints = new List<Waypoint>();

            foreach (MBRoute mbRoute in mbRoutes)
            {
                List<Geolocation> geometry = ConvertRouteGeometry(mbRoute.Geometry);

                Route route = new Route(geometry, mbRoute.Duration, mbRoute.Distance);

                routes.Add(route);
            }

            foreach (MBWaypoint mbWaypoint in mbWaypoints)
            {
                Waypoint waypoint = new Waypoint(
                    mbWaypoint.Name,
                     LocationTypeConverter.Vector2ddToGeolocation(mbWaypoint.Location)
                     );
                waypoints.Add(waypoint);
            }

            return new DirectionsResponse(routes,waypoints);

        }

        private List<Geolocation> ConvertRouteGeometry(List<Vector2d> mbRouteGeometry)
        {
            return mbRouteGeometry
            .Select(x =>
            {
                return LocationTypeConverter.Vector2ddToGeolocation(x);
            })
            .ToList();
        }
    }
}