using System.Collections.Generic;

namespace OurWorld.Scripts.DataModels.MapAPIResponses.DirectionsResponseModels
{
    public class DirectionsResponse
    {
        public List<Route> Routes { get; set; }

        public List<Waypoint> Waypoints { get; set; }

        public DirectionsResponse()
        {
            Routes = new List<Route>();
            Waypoints = new List<Waypoint>();
        }

        public DirectionsResponse(List<Route> routes, List<Waypoint> waypoints) 
        {
            Routes = routes;
            Waypoints = waypoints;
        }
    }
}