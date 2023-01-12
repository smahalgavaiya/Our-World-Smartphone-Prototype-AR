using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateTiles : MonoBehaviour
{


    public int board_size_x_;
    public int board_size_z_;
    public Transform tile_prefab_;
    public GameObject[,] allGrids;




    // Use this for initialization
    void Start()
    {
        allGrids = new GameObject[board_size_x_, board_size_z_];

        for (int x = 0; x < board_size_x_; x++)
        {
            for (int z = 0; z < board_size_z_; z++)
            {
                // Transform tile = (Transform)Instantiate(tile_prefab_, new Vector3(x*1.1f, 0, z*1.1f), Quaternion.Euler(0,0,0));
                Transform tile = (Transform)Instantiate(tile_prefab_, new Vector3(x * 36f,z * 36f,0)+gameObject.transform.position, Quaternion.Euler(0, 0, 0));
                allGrids[x, z] = tile.gameObject;
                tile.name = "Tile" + x + z;
                tile.parent = gameObject.transform;
               
            }
        }
    }
}
