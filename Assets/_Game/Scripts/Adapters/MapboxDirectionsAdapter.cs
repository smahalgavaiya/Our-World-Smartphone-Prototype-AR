using OurWorld.Scripts.DataModels.GeolocationData;

namespace OurWorld.Scripts.Adapters
{
    public class MapboxDirectionsAdapter
    {
        public Task<DirectionsResponse> GetDirectionsAsync()
        {

        }
    }

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

    public class Route
    {

    }

    public class Waypoint
    {
        public readonly string Name;

        public readonly Geolocation Location;

        public Waypoint(string name, Geolocation location)
        {
            Name = name;
            Location = location;
        }

        public Waypoint(string name, double lat, double lon):this(name,new Geolocation(lon,lat))
        {

        }
    }
}