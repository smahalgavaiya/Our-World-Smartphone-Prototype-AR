using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectionManger : MonoBehaviour
{
    public bool isSelected = false;
    public GameObject initialselGrid;
    public GameObject lastGrid;
    public GenerateTiles generateTiles;
    public int[] first, last;
    public int x_first, y_first, x_last, y_last;
    public List<GameObject> currentSelection;
    public Button clearAllButton;
    // Start is called before the first frame update


    [SerializeField] GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    [SerializeField] EventSystem m_EventSystem;
    [SerializeField] RectTransform canvasRect;

    void Start()
    {
        generateTiles = GetComponent<GenerateTiles>();
        clearAllButton.onClick.AddListener(clearAllSelection);
    }

    // Update is called once per frame
    void Update() 
    {
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the game object
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);


        if (Input.GetMouseButtonDown(0))
        {
            if (isSelected)
            {
                isSelected = false;
                initialselGrid = null;
                currentSelection.Clear();
            }

            else
            {
                currentSelection.Clear();
                isSelected = true;

                if (results.Count > 0) initialselGrid=results[0].gameObject;
               
            }
                

           
        }

        if (isSelected)
        {

            if (results.Count > 0) lastGrid= results[0].gameObject;


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


                    
                    //case 1 north 

                    if (y_last > y_first && x_first == x_last)
                    {
                        clearSelection();
                        for (int i = y_first; i <= y_last; ++i)
                        {
                            generateTiles.allGrids[x_last, i].GetComponent<singleGrid>().Gridselected();
                            addToSelection(generateTiles.allGrids[x_last, i]);
                        }
                    }

                    //case 2  north-east 
                    else if (x_first < x_last && y_first < y_last)
                    {
                        clearSelection();
                        for (int i = x_first; i <= x_last; ++i)
                        {
                            for (int j = y_first; j <= y_last; ++j)
                            {
                                generateTiles.allGrids[i, j].GetComponent<singleGrid>().Gridselected();
                                addToSelection(generateTiles.allGrids[i, j]);
                            }
                          
                        }
                    }

                    //case 3 east 
                    else if ( x_last> x_first && y_first == y_last)
                    {
                        clearSelection();
                        for (int i = x_first; i <= x_last; ++i)
                        {
                            generateTiles.allGrids[i, y_first].GetComponent<singleGrid>().Gridselected();
                            addToSelection(generateTiles.allGrids[i, y_first]);
                        }
                    }

                    //case 4 south-east 
                    else if (x_first < x_last && y_first > y_last)
                    {
                        clearSelection();
                        for (int i = x_first; i <= x_last; ++i)
                        {
                            for (int j = y_last; j <= y_first; ++j)
                            {
                                generateTiles.allGrids[i, j].GetComponent<singleGrid>().Gridselected();
                                addToSelection(generateTiles.allGrids[i, j]);
                            }

                        }
                    }
                   
                    //case 5 south
                    else if (y_first > y_last && x_first == x_last)
                    {
                        clearSelection();
                        for (int i = y_last; i <= y_first; ++i)
                        {
                            generateTiles.allGrids[x_last, i].GetComponent<singleGrid>().Gridselected();
                            addToSelection(generateTiles.allGrids[x_last, i]);
                        }
                    }

                    //case 6 south-west
                    else if (x_first > x_last && y_first > y_last)
                    {
                        clearSelection();
                        for (int i = x_last; i <= x_first; ++i)
                        {
                            for (int j = y_last; j <= y_first; ++j)
                            {
                                generateTiles.allGrids[i, j].GetComponent<singleGrid>().Gridselected();
                                addToSelection(generateTiles.allGrids[i, j]);
                            }

                        }
                    }

                    //case 7 west 
                    else if (x_first > x_last && y_first == y_last)
                    {
                        clearSelection();
                        for (int i = x_last; i <= x_first; ++i)
                        {
                            generateTiles.allGrids[i, y_first].GetComponent<singleGrid>().Gridselected();
                            addToSelection(generateTiles.allGrids[i, y_first]);
                        }
                    }

                    //case 8  north-west
                    else if (x_first > x_last && y_first < y_last)
                    {
                        clearSelection();
                        for (int i = x_last; i <= x_first; ++i)
                        {
                            for (int j = y_first; j <= y_last; ++j)
                            {
                                generateTiles.allGrids[i, j].GetComponent<singleGrid>().Gridselected();
                                addToSelection(generateTiles.allGrids[i, j]);
                            }

                        }
                    }
                }
            }
        }
    }

    void addToSelection(GameObject selectedGB)
    {
        currentSelection.Add(selectedGB);
    }
    void clearSelection()
    {
        foreach (GameObject item in currentSelection)
        {
            item.GetComponent<singleGrid>().Gridunselected();
        }
       
        currentSelection.Clear();
    }

    public void clearAllSelection()
    {
        Debug.Log("clear all seelction ");
        for (int x = 0; x < generateTiles.board_size_x_; x++)
        {
            for (int z = 0; z < generateTiles.board_size_z_; z++)
            {

                generateTiles.allGrids[x,z].GetComponent<singleGrid>().Gridunselected();
            }
        }
        currentSelection.Clear();
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
