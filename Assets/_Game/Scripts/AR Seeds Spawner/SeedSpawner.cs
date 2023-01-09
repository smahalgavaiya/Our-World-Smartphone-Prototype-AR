using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OurWorld.Scripts.DataModels;
using OurWorld.Scripts.DataModels.GeolocationData;
using Cysharp.Threading.Tasks;
using OurWorld.Scripts.Interfaces.MapAPI;
using UnityEngine.SceneManagement;

public class SeedSpawner : MonoBehaviour
{
    public Vector3 spawnPoint;
    public GameObject[] SeedTypes;

    public Vector3 seedSpawnV3;
    public GameObject seedObject;


    private IMapAPIProvider _mapApiProvider;

    // Start is called before the first frame update
    async void Start()
    {

        spawnPoint = Vector3.zero;
        StartCoroutine(StartSpawning());

        //Spawn_Seeds(await _mapApiProvider.GetNearbyParksAsync(new Geolocation(32.707270, 39.995767), 0.5f));
    }

    public void openUnityWorldSpace()
    {
        SceneManager.LoadScene("UnityWorldSpace");
    }



    IEnumerator StartSpawning()
    {
        spawnPoint = new Vector3(Random.Range(0, 100f), 0, Random.Range(0, 100f));
        yield return new WaitForSeconds(3);
        for (int i = 0; i < SeedTypes.Length; i++)
        {
            Instantiate(SeedTypes[i], spawnPoint, Quaternion.identity);
        }
        StartCoroutine(StartSpawning());

    }

    public void Spawn_Seeds(List<ParkData> parkDist)
    {
        //player is in park
        if (parkDist.Count >= 0)
        {
            for (int i = 0; i < 100; i++)
            {
                seedSpawnV3 = new Vector3(Random.Range(0, 100f), 0, Random.Range(0, 100f));
                Instantiate(seedObject, seedSpawnV3, Quaternion.identity);
            }
        }
        else
        {
            Debug.Log("Not in Park");
        }

    }

}

