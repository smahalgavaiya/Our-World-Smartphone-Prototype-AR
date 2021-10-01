using OurWorld.Scripts.DataModels.GeolocationData;

namespace OurWorld.Scripts.DataModels
{
    public class ParkData
    {
        public readonly string Name;
        public readonly Geolocation Geolocation;
        
        /// <summary>
		/// Distance from player to park in kilometers
		/// </summary>
        public float Distance = 0f;

        public ParkData(string name, Geolocation geolocation)
        {
            Name = name;
            Geolocation = geolocation;
        }
    }
}