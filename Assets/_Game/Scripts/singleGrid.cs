using UnityEngine;
using UnityEngine.UI;

public class singleGrid : MonoBehaviour
{
    //MeshRenderer gridMat;
    Image singleGridImage;

    Color translucent = new Color(1, 1, 1, 0.2f);
    Color transparent= new Color(1, 1, 1, 0.45f);

    private void Start()
    {
        singleGridImage = GetComponent<Image>();
        singleGridImage.color = translucent;
        
     /*   gridMat = GetComponent<MeshRenderer>();
        gridMat.material.color = translucent;*/
    }

    public void Gridselected()
    {
        // gridMat.material.color = transparent;
        singleGridImage.color = transparent;
    }
    public void Gridunselected()
    {
        // gridMat.material.color = translucent;
        singleGridImage.color = translucent;
    }

    public void checkGrid()
    {
        // if (gridMat.material.color == transparent)
        if (singleGridImage.color == transparent)
            Gridunselected();
        else
            Gridselected();
    }
    /*
    void OnMouseDown()
    {
        if (gridMat.material.color == Color.red)
            Gridunselected();
        else
            Gridselected();
    }
    */
}
