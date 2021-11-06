using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using OurWorld.Scripts.DataModels;
using OurWorld.Scripts.DataModels.GeolocationData;

namespace OurWorld.Scripts.Interfaces.MapAPI
{
    public interface IMapAPIProvider
    {
        IDirectionsAPIProvider DirectionsAPI { get; }
        
        UniTask<List<ParkData>> GetNearbyParksAsync(Geolocation playerLocation, float radius);
    }
}
