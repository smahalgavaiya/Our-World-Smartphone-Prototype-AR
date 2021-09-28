using OurWorld.Scripts.DataModels.GeolocationData;
using OurWorld.Scripts.Extensions;
using OurWorld.Scripts.Utilities;
using UnityEngine;
namespace OurWorld.Scripts.Controllers
{

    public class GameController : MonoBehaviour
    {

        private async void Awake()
        {
           var apiProvider = new MapboxAPIProvider();

           await apiProvider.GetNearbyParksAsync(new Geolocation(32.707270,39.995767),1f);
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