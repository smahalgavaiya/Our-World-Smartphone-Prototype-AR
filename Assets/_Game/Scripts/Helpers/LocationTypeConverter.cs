using Mapbox.Utils;
using OurWorld.Scripts.DataModels.GeolocationData;

namespace OurWorld.Scripts.Helpers
{
    public static class LocationTypeConverter
    {
        
        public static Geolocation Vector2ddToGeolocation(Vector2d vector)
        {
                return new Geolocation(vector.y,vector.x);
        }
        
        public static Vector2d GeolocationToVector2d(Geolocation geolocation)
        {
            return new Vector2d(y:geolocation.Longitude,x:geolocation.Latitude);
        }
    }
}