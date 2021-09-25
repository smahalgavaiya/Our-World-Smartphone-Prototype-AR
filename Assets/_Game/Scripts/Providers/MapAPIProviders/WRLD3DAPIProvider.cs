using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using OurWorld.Scripts.DataModels;
using OurWorld.Scripts.DataModels.GeolocationData;
using OurWorld.Scripts.Interfaces.MapAPI;

public class WRLD3DAPIProvider : IMapAPIProvidder
{
    public UniTask<List<ParkData>> GetNearbyParksAsync(Geolocation playerLocation,float radius)
    {
        throw new System.NotImplementedException();
    }
}
