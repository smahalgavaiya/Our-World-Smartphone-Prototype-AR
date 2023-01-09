namespace OurWorld.Scripts.Interfaces.MapAPI
{
    public interface IMapAPIProviderFactory
    {
        public IMapAPIProvider Provider { get; }

        public IDirectionsAPIProvider Directions {get;}
    }
}