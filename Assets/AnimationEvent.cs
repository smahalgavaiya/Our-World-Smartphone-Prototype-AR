using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public SeedCollect seedCollect;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
   public  void PickupComplete()
    {
        seedCollect.PickupComplete();
    }
    public  void Picking()
    {
        seedCollect.Picking();
    }

}
