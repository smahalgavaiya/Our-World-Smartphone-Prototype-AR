using System.Collections.Generic;

namespace OurWorld.Scripts.DataModels.MapAPIResponses.DirectionsResponseModels
{
    public class DirectionsResponse
    {
        public List<Route> Routes;

        public List<Waypoint> Waypoints;

        public DirectionsResponse()
        {
            Routes = new List<Route>();
            Waypoints = new List<Waypoint>();
        }
    }
}