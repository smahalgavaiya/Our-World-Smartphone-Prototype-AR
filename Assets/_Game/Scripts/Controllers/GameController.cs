using OurWorld.Scripts.DataModels.Geolocation;
using OurWorld.Scripts.Utilities;
using UnityEngine;
namespace OurWorld.Scripts.Controllers
{

    public class GameController : MonoBehaviour
    {

        private void Awake()
        {
            var bb = new Geolocation(32.70690685783961d,39.995918554330984d).GetBoundingBox(0.5d);

            Debug.Log(bb.ToString());
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