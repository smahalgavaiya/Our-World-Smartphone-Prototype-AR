using UnityEngine;
using System.Collections;


public class GenerateTiles : MonoBehaviour
{


    public int board_size_x_;
    public int board_size_z_;
    public Transform tile_prefab_;


    // Use this for initialization
    void Start()
    {
        GameObject board = new GameObject();
        board.name = "Board";
        for (int x = 0; x < board_size_x_; x++)
        {
            for (int z = 0; z < board_size_z_; z++)
            {
                Transform tile = (Transform)Instantiate(tile_prefab_, new Vector3(x, 0, z), Quaternion.Euler(90,0,0));
                tile.name = "Tile" + x + z;
                tile.parent = board.transform;
            }
        }
    }
}
