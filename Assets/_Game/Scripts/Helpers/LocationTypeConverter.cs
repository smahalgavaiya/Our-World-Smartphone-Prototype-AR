using Mapbox.Utils;
using OurWorld.Scripts.DataModels.GeolocationData;

namespace OurWorld.Scripts.Helpers
{
    public static class LocationTypeConverter
    {
        
        public static Geolocation Vector2ddToGeolocation(Vector2d vector, bool isVectorlatLonOrder = true)
        {
            if(isVectorlatLonOrder)
                return new Geolocation(vector.y,vector.x);
            else
                return new Geolocation(vector.x,vector.y);
        }
        
        public static Vector2d GeolocationToVector2d(Geolocation geolocation, bool latLonOrder = true)
        {
            if(latLonOrder)
                return new Vector2d(geolocation.Latitude,geolocation.Longitude);
            else
                return new Vector2d(geolocation.Longitude,geolocation.Latitude);
        }
    }
}