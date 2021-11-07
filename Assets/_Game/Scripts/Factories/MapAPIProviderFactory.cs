using OurWorld.Scripts.Interfaces.MapAPI;

namespace OurWorld.Scripts.Factories
{
    public class MapAPIProviderFactory : IMapAPIProviderFactory
    {
        private IMapAPIProvider _provider;

        public IMapAPIProvider Provider => throw new System.NotImplementedException();

        public IDirectionsAPIProvider Directions => throw new System.NotImplementedException();

        public static IMapAPIProviderFactory Instance { get; private set; }

        public MapAPIProviderFactory(IMapAPIProvider provider)
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Debug.LogError("Trying to create new instance of a singleton. This is not allowed");
            }
            _provider = provider;
        }
    }
}