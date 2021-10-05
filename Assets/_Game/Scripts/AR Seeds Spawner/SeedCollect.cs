using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedCollect : MonoBehaviour
{
    public GameObject arCam;
    public GameObject Particle;
    public  GameObject PlayerModel;
    public  GameObject nonStaticPlayerModel;
    public  GameObject rHand;
    private  GameObject Litter;
    private Animator animator;
    public float speed;
    public float dist;
    bool litterFound;
    float startingY;
    // Start is called before the first frame update
    private void Start()
    {
        animator = PlayerModel.GetComponent<Animator>();
       PlayerModel=  nonStaticPlayerModel;
    }
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
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (litterFound)
        {
           float distX = PlayerModel.transform.position.x - Litter.transform.position.x;
           float distZ = PlayerModel.transform.position.z - Litter.transform.position.z;
            if (distX > 0.3f )
            {
                float step = speed * Time.deltaTime;
                Vector3 dir = Litter.transform.position - PlayerModel.transform.position;
                /*float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                PlayerModel.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);*/
              
                Vector3 targetPostition = new Vector3(Litter.transform.position.x,
                                        PlayerModel.transform.position.y,
                                        Litter.transform.position.z);
                PlayerModel.transform.LookAt(targetPostition);

              /*  Quaternion rotation = Quaternion.LookRotation(dir);
                PlayerModel.transform.rotation = Quaternion.EulerAngles(0, (dir) rotation.y, 0);*/

                /*PlayerModel.transform.LookAt(Litter.transform);*/

                Vector3 updatedPos = new Vector3(Litter.transform.position.x,startingY, Litter.transform.position.z);
                if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                animator.Play("Walk");
                PlayerModel.transform.position = Vector3.MoveTowards(PlayerModel.transform.position, updatedPos - new Vector3(0f, 0, 0), step);


            }
            else
            {
                animator.Play("Pickup");
/*                litterFound = false;*/
            }
        }
     if (Input.GetMouseButton(0) && !litterFound)
            {
             startingY = PlayerModel.transform.position.y;


            if (Physics.Raycast(ray, out hit,100f))
            {
                if (hit.transform.tag == "Litter")
                {
                    Litter = hit.transform.gameObject;
                    PlayerModel.SetActive(true);
                    /* Transform hand = PlayerModel.transform.Find("RightHand");
                     PlayerModel.transform.*/
                    /*  Destroy(hit.transform.gameObject, 0.5f);*/
                    if (!litterFound)
                    {
                        var particle = Instantiate(Particle, hit.point, Quaternion.LookRotation(hit.normal));
                        Destroy(particle, 1f);
                    }
                    litterFound = true;

                }
                
            }

        }
}
    public void Picking()
    {
        Litter.transform.gameObject.transform.SetParent(rHand.transform);/*
                hit.transform.GetComponentInChildren<FixedJoint>().connectedBody = rHand.GetComponent<Rigidbody>();*/
        Litter.transform.gameObject.transform.localPosition = Vector3.zero;
       
    }
    public void PickupComplete()
    {
        PlayerModel.transform.position = new Vector3(Random.Range(0, 10), startingY, Random.Range(0, 10));
        Destroy(Litter, 0.5f);
        litterFound = false;
    }
}
