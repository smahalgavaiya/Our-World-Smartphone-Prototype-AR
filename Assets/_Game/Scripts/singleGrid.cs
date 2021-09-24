using UnityEngine;

public class singleGrid : MonoBehaviour
{
    MeshRenderer gridMat;

    private void Start()
    {
        gridMat = GetComponent<MeshRenderer>();
        gridMat.material.color = Color.blue;
    }

    void Gridselected()
    {
        gridMat.material.color = Color.red;
    }
    void Gridunselected()
    {
        gridMat.material.color = Color.blue;
    }

    void OnMouseDown()
    {
        if (gridMat.material.color == Color.red)
            Gridunselected();
        else
            Gridselected();
    }
}
