using System.Collections.Generic;
using OurWorld.Scripts.DataModels.GeolocationData;

namespace OurWorld.Scripts.Interfaces.MapAPI.Directions
{
    public interface INavigationBehaviour
    {
        public bool Active { get; }

        public void StartNavigation(List<Geolocation> waypoints);

        public void DisposeActiveNavigation();

        public void UpdateRoute(List<Geolocation> waypoints);

        public void Update();
    }
}