using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using OurWorld.Scripts.DataModels;
using OurWorld.Scripts.DataModels.GeolocationData;
using OurWorld.Scripts.Interfaces.MapAPI;

namespace OurWorld.Scripts.Providers.MapAPIProviders
{
    public class WRLD3DAPIProvider : IMapAPIProvider
    {
        public IDirectionsAPIProvider DirectionsAPI => throw new System.NotImplementedException();

        public UniTask<List<ParkData>> GetNearbyParksAsync(Geolocation playerLocation, float radius)
        {
            throw new System.NotImplementedException();
        }
    }
}
