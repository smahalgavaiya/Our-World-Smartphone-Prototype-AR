using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using OurWorld.Scripts.DataModels;
using OurWorld.Scripts.DataModels.GeolocationData;
using OurWorld.Scripts.Interfaces.MapAPI.Geocoding;

namespace OurWorld.Scripts.Interfaces.MapAPI
{
    public interface IMapAPIProvider
    {
        IDirectionsAPIProvider DirectionsAPI { get; }

        IForwardGeocodingProvider GeocodingProvider {get;}
    }
}
