using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff : MonoBehaviour
{

    [SerializeField]
    private GameObject ObjectToToggle;
   public void Toggle()
    {
        ObjectToToggle.SetActive(!ObjectToToggle.active);
    }

 
}
