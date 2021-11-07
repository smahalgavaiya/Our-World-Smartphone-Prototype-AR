using System.Collections.Generic;
using OurWorld.Scripts.Interfaces.MapAPI.Geocoding;

namespace OurWorld.Scripts.DataModels.MapAPIResponses
{
    public class ForwardGeocodingResponse<T> where T : IPointOfInterest
    {
        public readonly List<T> Places;

        public readonly bool IsSuccess;

        public ForwardGeocodingResponse(bool isSuccess,List<T> places)
        {
            IsSuccess = isSuccess;
            Places = places;
        }
    }
}