using System.Collections.Generic;
using OurWorld.Scripts.DataModels;
using OurWorld.Scripts.DataModels.Geolocation;

namespace OurWorld.Scripts.Interfaces.MapAPI
{
    public interface IMapAPIProvidder
    {
        List<ParkData> GetNearbyParks(Geolocation playerLocation);
    }
}
