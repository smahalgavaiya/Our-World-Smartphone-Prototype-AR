using OurWorld.Scripts.DataModels.GeolocationData;

namespace OurWorld.Scripts.DataModels.MapAPIResponses.DirectionsResponseModels
{
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