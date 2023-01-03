using System;
using OurWorld.Scripts.Interfaces.MapAPI;
using OurWorld.Scripts.Providers.MapAPIProviders;
using OurWorld.Scripts.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OurWorld.Scripts.Controllers
{
    public class GameController : MonoBehaviour
    {
        public static event Action GameInitialized;

        [SerializeField] private NearbyPlacesController _nearbyPlacesController;

        private bool _initialized;     
        public bool Initialized => _initialized;
        
        private static IMapAPIProvider _mapApiProvider;
        public static IMapAPIProvider MapAPIProvidder => _mapApiProvider;
        private void Awake()
        {
           // Initialize();
        }
        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            //Uncomment below when WRLD3D is ready
            int mapNum = PlayerPrefs.GetInt("PreferredMap");
            //Mapbox
            if (mapNum == 0)
            {

                _mapApiProvider = new MapboxAPIProvider();
            }
            //WRLD3D
            else if (mapNum == 1)
            {
                _mapApiProvider = new WRLD3DAPIProvider();
            }

           // _mapApiProvider = new MapboxAPIProvider();

            _nearbyPlacesController.Initialize(_mapApiProvider);

            _initialized = true;

            GameInitialized?.Invoke();
        }

        public void openSeedsParkScene()
        {
            SceneManager.LoadScene("Seed Spawn");
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