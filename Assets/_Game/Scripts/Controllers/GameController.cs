using System;
using Cysharp.Threading.Tasks;
using OurWorld.Scripts.DataModels.GeolocationData;
using OurWorld.Scripts.Extensions;
using OurWorld.Scripts.Interfaces.MapAPI;
using OurWorld.Scripts.Providers.MapAPIProviders;
using OurWorld.Scripts.Utilities;
using UnityEngine;
namespace OurWorld.Scripts.Controllers
{
    public class GameController : MonoBehaviour
    {
        public static event Action GameInitialized;
        private IMapAPIProvider _mapApiProvider;
        private bool _initialized;
        public bool Initialized => _initialized;
        public IMapAPIProvider MapAPIProvidder => _mapApiProvider;
        private void Awake()
        {
            Initialize();
        }

        private async void Initialize()
        {
            _mapApiProvider = new MapboxAPIProvider();

            await _mapApiProvider.GetNearbyParksAsync(new Geolocation(32.707270, 39.995767), 1f);

            _initialized = true;

            GameInitialized?.Invoke();
        }

        public void DoAfterInitialize(Action callBack)
        {
            if (Initialized) callBack?.Invoke();

            GameInitialized += OnInitialize;

            void OnInitialize()
            {
                GameInitialized -= OnInitialize;
                callBack?.Invoke();
            }
        }


#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
                CrossfadeTransition.Instance.FadeIn(null);
            if (Input.GetKeyDown(KeyCode.P))
                CrossfadeTransition.Instance.FadeOut(null);
        }
#endif
    }

}