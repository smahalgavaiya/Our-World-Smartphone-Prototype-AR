using System.Collections.Generic;

namespace OurWorld.Scripts.DataModels.MapAPIResponses
{
    public class ForwardGeocodingResponse<T> where T : POIData
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