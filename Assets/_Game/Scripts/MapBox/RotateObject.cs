using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
	// Start is called before the first frame update
	public Vector3 RotateAmount = new Vector3(0,10f,0);  // degrees per second to rotate in each axis. Set in inspector.
    public bool Play= true;

    // Update is called once per frame
    void Update()
    {
        Debug.Log("move");
        if(Play)
        transform.Rotate(RotateAmount * Time.deltaTime);
    }
}
