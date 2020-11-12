using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Wrld.Space;
using UnityEngine.UI;
using TMPro;

public class GPSLocationProvider : MonoBehaviour
{
    public bool IsReady = false;

    public event Action<LatLong> OnLocationUpdated;

    [SerializeField]
    public TMP_Text LocationDataText;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        LocationDataText.text = "GPS Loading";

        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            LocationDataText.text = "GPS Disabled";
            yield break;
        }

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            LocationDataText.text = "GPS Timed out";
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            LocationDataText.text = "GPS Failed";
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            LocationDataText.text = "Swtich To GPS";

            IsReady = true;
            while (Input.location.status == LocationServiceStatus.Running)
            {
                LatLong latlong = LatLong.FromDegrees(Input.location.lastData.latitude, Input.location.lastData.longitude);
                
                    OnLocationUpdated?.Invoke(latlong);

                yield return new WaitForSeconds(1);
            }
        }

    }
}
