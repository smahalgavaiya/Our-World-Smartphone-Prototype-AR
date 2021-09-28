using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedCollect : MonoBehaviour
{
    public GameObject arCam;
    public GameObject Particle;
    public GameObject PlayerModel;
    // Start is called before the first frame update
    public void Collect()
    {
        RaycastHit hit;
        if (Physics.Raycast(arCam.transform.position, arCam.transform.forward, out hit))
        {
            if (hit.transform.tag == "Seed") {
                PlayerModel.SetActive(true);
                Destroy(hit.transform.gameObject, 0.5f);
                Instantiate(Particle, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
        if (Physics.Raycast(arCam.transform.position, arCam.transform.forward, out hit))
        {
            if (hit.transform.tag == "Litter")
            {
                PlayerModel.SetActive(true);
                Transform hand = PlayerModel.transform.Find("Hand");
                hit.transform.gameObject.transform.SetParent(hand);

                Destroy(hit.transform.gameObject, 0.5f);
                Instantiate(Particle, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
    private void Update()
    {
        
     if (Input.GetMouseButton(0))
            { 
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit,100f))
            {
                if (hit.transform.tag == "Litter")
                {
                    PlayerModel.SetActive(true);
                    Transform hand = PlayerModel.transform.Find("LeftHand");
                    hit.transform.gameObject.transform.SetParent(hand);
                    hit.transform.GetComponentInChildren<FixedJoint>().connectedBody = PlayerModel.transform.gameObject.GetComponent<Rigidbody>();
                    hit.transform.gameObject.transform.position = Vector3.zero;

                    /*  Destroy(hit.transform.gameObject, 0.5f);*/
                    var particle=   Instantiate(Particle, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(particle, 1f);
                }
                
            }

        }
}
}
