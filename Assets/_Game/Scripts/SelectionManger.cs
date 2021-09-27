using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManger : MonoBehaviour
{
    public bool isSelected = false;
    public GameObject initialselGrid;
    public GameObject lastGrid;
    public GenerateTiles generateTiles;
    public int[] first, last;
    public int x_first, y_first, x_last, y_last;
    // Start is called before the first frame update
    void Start()
    {
        generateTiles = GetComponent<GenerateTiles>();
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
                    lastGrid = hit.transform.gameObject;
                }
            

             first = CoordinatesOf(generateTiles.allGrids, initialselGrid);
             last = CoordinatesOf(generateTiles.allGrids, lastGrid);

            if (first != null && last != null)
            {
                if (last.Length == 2
                    &&
                    first.Length == 2)

                {
                    x_first = first[0];
                    y_first = first[1];

                    x_last = last[0];
                    y_last = last[1];



                    //case one north 

                    if (x_first > x_last && y_first == y_last)
                    {
                        for (int i = x_last; i <= x_first; ++i)
                        {
                            generateTiles.allGrids[i, y_first].GetComponent<singleGrid>().Gridselected();
                        }
                    }

                    //case two  north-east 

                    //case three east 

                    //case four south-east 

                    //case five south

                    //case six south-west

                    //case seven west 

                    //case 8  north-west
                }
            }
        }
    }

    public int[] CoordinatesOf(GameObject[,] matrix, GameObject value)
    {
        if(value==null)
            return null;
        int w = matrix.GetLength(0); // width
        int h = matrix.GetLength(1); // height

        for (int x = 0; x < w; ++x)
        {
            for (int y = 0; y < h; ++y)
            {
                if (matrix[x,y].Equals(value))
                {
                    int[] arr = new int[2];
                    arr[0] = x;
                    arr[1] = y;
                    return arr;
                }

            }
        }
        return null;
    }
}
