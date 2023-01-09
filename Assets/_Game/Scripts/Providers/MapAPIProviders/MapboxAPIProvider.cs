
using OurWorld.Scripts.Helpers;
using OurWorld.Scripts.Interfaces;
using OurWorld.Scripts.Interfaces.MapAPI;
using OurWorld.Scripts.Utilities.DataSerializers;
using OurWorld.Scripts.Interfaces.MapAPI.Geocoding;

namespace OurWorld.Scripts.Providers.MapAPIProviders
{
    public class MapboxAPIProvider : IMapAPIProvider
    {
        public const string BaseAPIUrl = "https://api.mapbox.com/";
        public const string ApiToken = "pk.eyJ1IjoiYmxhY3F2dmUiLCJhIjoiY2t0cTNlbDdsMHNueDJvcXUzNGFtMmI5aiJ9.d3FBlEYgLcTn-bfqV2uXrQ";
        private readonly IWebRequestHelper _webRequestHelper;
        private readonly IDirectionsAPIProvider _directionsAPIProvider;
        private readonly IForwardGeocodingProvider _geocodingProvider;

        public IDirectionsAPIProvider DirectionsAPI => _directionsAPIProvider;

        public IForwardGeocodingProvider GeocodingProvider => _geocodingProvider;

        public MapboxAPIProvider()
        {
            _webRequestHelper = new WebRequestHelper(new MapBoxNewtonsoftJsonSerializerOption());
            _directionsAPIProvider = new MapboxDirectionsAPIProvider();
            _geocodingProvider = new MapboxForwardGeocodingAPIProvider(_webRequestHelper);
        } 
    }
}



