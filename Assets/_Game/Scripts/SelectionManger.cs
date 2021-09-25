using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManger : MonoBehaviour
{
    public bool isSelected = false;
    public GameObject initialselGrid;
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    { 

        if (Input.GetMouseButtonDown(0))
        {
            if (isSelected)
            {
                isSelected = false;
                initialselGrid = null;

            }

            else
            {
                isSelected = true;

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                    if (hit.collider != null)
                    {
                        initialselGrid = hit.transform.gameObject;
                    }
            }
                

           
        }

        if (isSelected)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
                if (hit.collider != null)
                {
                    hit.transform.gameObject.GetComponent<singleGrid>().Gridselected();
                }

        }
    }
}
