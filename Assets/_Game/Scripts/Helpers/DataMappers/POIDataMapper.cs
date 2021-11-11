using OurWorld.Scripts.DataModels;
using OurWorld.Scripts.DataModels.GeolocationData;
using OurWorld.Scripts.Interfaces;
using Feature =  Mapbox.Geocoding.Feature;

namespace OurWorld.Scripts.Helpers.DataMappers
{
    public class POIDataMapper : IDataMapper<Feature, POIData>
    {
        public POIData MapObject(Feature sourceObject)
        {
            string placeName = sourceObject.PlaceName.Split(',')[0];

            Geolocation centerCordinates = new Geolocation(sourceObject.Center.y,sourceObject.Center.x);

            string type = "";
            if(sourceObject.Properties.TryGetValue("category",out object categories))
            {
                type = (string)categories;
            }
            return new POIData(placeName,type,centerCordinates);
        }
    }
}