using System.Globalization;
using Mapbox.Utils;

namespace OurWorld.Scripts.DataModels.GeolocationData
{
    public class Geolocation
    {
        public double Longitude { get; set; } // In Degrees
        public double Latitude { get; set; } // In Degrees

        public Geolocation()
        {

        }
        public Geolocation(double longtitude, double latitude)
        {
            Longitude = longtitude;
            Latitude = latitude;
        }

        public override string ToString()
        {
            return $"{Longitude.ToString(CultureInfo.InvariantCulture)},{Latitude.ToString(CultureInfo.InvariantCulture)}";
        }

        public static implicit operator Vector2d(Geolocation location) => new Vector2d(location.Latitude,location.Longitude);
     }
}
