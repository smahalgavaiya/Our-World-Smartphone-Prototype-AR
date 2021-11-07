using OurWorld.Scripts.DataModels.GeolocationData;
using OurWorld.Scripts.Interfaces.MapAPI.Geocoding;

namespace OurWorld.Scripts.DataModels
{
    public class POIData : IPointOfInterest
    {
        public readonly string Name;
        public readonly string Type;
        public readonly Geolocation Geolocation;

        /// <summary>
        /// Distance from player to poi in kilometers
        /// </summary>
        public float Distance = 0f;
        
        public POIData(string name, string type, Geolocation geolocation)
        {
            Name = name;
            Type = type;
            Geolocation = geolocation;
        }

        public Geolocation GetGeolocation() => Geolocation;
    }
}