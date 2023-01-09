using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using OurWorld.Scripts.DataModels;
using OurWorld.Scripts.DataModels.MapAPIRequests;
using OurWorld.Scripts.DataModels.MapAPIResponses;

namespace OurWorld.Scripts.Interfaces.MapAPI.Geocoding
{
    public interface IForwardGeocodingProvider
    {
        UniTask<ForwardGeocodingResponse<IPointOfInterest>> POISearchAsync(IRequest request);

        UniTask<ForwardGeocodingResponse<IPointOfInterest>> BatchPOISearchAsync(IBatchRequest request);
        

    }
}