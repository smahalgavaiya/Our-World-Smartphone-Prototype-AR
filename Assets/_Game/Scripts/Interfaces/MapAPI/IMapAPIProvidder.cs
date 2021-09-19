using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using OurWorld.Scripts.DataModels;
using OurWorld.Scripts.DataModels.GeolocationData;

namespace OurWorld.Scripts.Interfaces.MapAPI
{
    public interface IMapAPIProvidder
    {
        UniTask<List<ParkData>> GetNearbyParksAsync(Geolocation playerLocation,float radius);
    }
}
