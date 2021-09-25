using UnityEngine;

public class singleGrid : MonoBehaviour
{
    MeshRenderer gridMat;

    Color translucent = new Color(1, 1, 1, 0.2f);
    Color transparent= new Color(1, 1, 1, 0.45f);

    private void Start()
    {
        gridMat = GetComponent<MeshRenderer>();
        gridMat.material.color = translucent;
    }

    public void Gridselected()
    {
        gridMat.material.color = transparent;
    }
    public void Gridunselected()
    {
        gridMat.material.color = translucent;
    }

    public void checkGrid()
    {
        if (gridMat.material.color == transparent)
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
