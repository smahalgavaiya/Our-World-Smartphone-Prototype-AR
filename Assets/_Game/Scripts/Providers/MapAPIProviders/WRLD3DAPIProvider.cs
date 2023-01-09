using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using OurWorld.Scripts.DataModels;
using OurWorld.Scripts.DataModels.GeolocationData;
using OurWorld.Scripts.Interfaces.MapAPI;
using OurWorld.Scripts.Interfaces.MapAPI.Geocoding;

namespace OurWorld.Scripts.Providers.MapAPIProviders
{
    public class WRLD3DAPIProvider : IMapAPIProvider
    {
        public IDirectionsAPIProvider DirectionsAPI => throw new System.NotImplementedException();

        public IForwardGeocodingProvider GeocodingProvider => throw new System.NotImplementedException();
    }
}
