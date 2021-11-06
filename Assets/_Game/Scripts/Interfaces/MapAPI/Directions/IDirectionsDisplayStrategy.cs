using System;
using System.Collections.Generic;
using OurWorld.Scripts.DataModels.GeolocationData;
using UnityEngine;

namespace OurWorld.Scripts.Interfaces.MapAPI
{
    public interface IDirectionsDisplayStrategy
    {
        public bool Active { get; }

        public void StartNavigation(List<Geolocation> waypoints,Func<Geolocation,Vector3> locationSolver);

        public void DisposeActiveNavigation();

        public void UpdateRoute(List<Geolocation> waypoints);
    }
}