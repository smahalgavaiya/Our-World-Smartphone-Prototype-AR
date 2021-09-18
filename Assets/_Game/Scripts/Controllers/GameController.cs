using System.Collections;
using System.Collections.Generic;
using OurWorld.Scripts.Utilities;
using UnityEngine;

public class GameController : MonoBehaviour
{
    
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
