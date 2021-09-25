using OurWorld.Scripts.DataModels;
using OurWorld.Scripts.DataModels.GeolocationData;
using OurWorld.Scripts.Interfaces;
using Feature =  Mapbox.Geocoding.Feature;

namespace OurWorld.Scripts.Helpers.DataMappers
{
    public class ParkDataMapper : IDataMapper<Feature, ParkData>
    {
        public ParkData MapObject(Feature sourceObject)
        {
            string placeName = sourceObject.PlaceName.Split(',')[0];

            Geolocation centerCordinates = new Geolocation(sourceObject.Center[0],sourceObject.Center[0]);

            return new ParkData(placeName,centerCordinates);
        }
    }
}