using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSpawner : MonoBehaviour
{
    public Vector3 spawnPoint;
    public GameObject[] SeedTypes;
    // Start is called before the first frame update
    void Start()
    { spawnPoint =  Vector3.zero;
        StartCoroutine(StartSpawning());
    }

  IEnumerator StartSpawning()
    {
        spawnPoint = new Vector3(Random.Range(0, 100f), 0, Random.Range(0, 100f));
       yield  return new WaitForSeconds(3);
        for (int i = 0; i < SeedTypes.Length; i++)
        {
            Instantiate(SeedTypes[i], spawnPoint, Quaternion.identity);
        }
        StartCoroutine(StartSpawning());
    }
}
