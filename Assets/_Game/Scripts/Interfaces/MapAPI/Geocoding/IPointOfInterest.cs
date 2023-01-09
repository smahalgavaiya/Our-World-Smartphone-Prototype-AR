using OurWorld.Scripts.DataModels.GeolocationData;

namespace OurWorld.Scripts.Interfaces.MapAPI.Geocoding
{
    public interface IPointOfInterest
    {
         Geolocation GetGeolocation();
    }
}