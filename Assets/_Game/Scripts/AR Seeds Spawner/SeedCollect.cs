using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedCollect : MonoBehaviour
{
    public GameObject arCam;
    public GameObject Particle;
    // Start is called before the first frame update
   public void Collect()
    {
        RaycastHit hit;
        if(Physics.Raycast(arCam.transform.position,arCam.transform.forward , out hit))
        {
            if (hit.transform.tag == "Seed") {
                Destroy(hit.transform.gameObject);
                Instantiate(Particle, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }

}
