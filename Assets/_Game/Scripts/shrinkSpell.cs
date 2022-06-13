using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shrinkSpell : MonoBehaviour
{
    // Start is called before the first frame update
    float size = 0.005f;
    void Start()
    {

       // StartCoroutine(shrinkCoroutine());
    }

    IEnumerator shrinkCoroutine()
    {
        while (transform.localScale.x>= 0.004)
        {
            transform.localScale = Vector3.Lerp(this.transform.localScale, this.transform.localScale - new Vector3(size, size, size), .5f);
            
            yield return new WaitForSeconds(.5f);
        }
        
     }
}
