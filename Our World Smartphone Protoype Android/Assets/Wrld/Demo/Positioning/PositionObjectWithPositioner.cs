using System.Collections;
using UMA.CharacterSystem;
using UnityEngine;
using UnityEngine.UI;
using Wrld;
using Wrld.Space;
using Wrld.Space.Positioners;

public class PositionObjectWithPositioner: MonoBehaviour
{
    static LatLong targetPosition = LatLong.FromDegrees(37.783372, -122.400834);
    
    public Transform box;
    public DynamicCharacterAvatar avatar;
  //  GeographicTransform geographicTransformAvatar;
    Positioner boxPositioner;
    public Text LatLongText;

    IEnumerator Start()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

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
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            LatLongText.GetComponent<Text>().text = "Unable to determine device location";
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);

            LatLongText.GetComponent<Text>().text = "Location: Lat: " + Input.location.lastData.latitude + " Long: " + Input.location.lastData.longitude + " Altitute: " + Input.location.lastData.altitude + " HorrizontalAccuracy: " + Input.location.lastData.horizontalAccuracy + " Timestamp: " + Input.location.lastData.timestamp;
        }

        // Stop service if there is no need to query location updates continuously
        //Input.location.Stop();
    }

    private void OnEnable()
    {


        var positionerOptions = new PositionerOptions()
                                                .ElevationAboveGround(0)
                                                .LatitudeDegrees(targetPosition.GetLatitude())
                                                .LongitudeDegrees(targetPosition.GetLongitude());

        //var positionerOptions = new PositionerOptions()
        //                                        .ElevationAboveGround(0)
        //                                        .LatitudeDegrees(Input.location.lastData.latitude)
        //                                        .LongitudeDegrees(Input.location.lastData.longitude);

        boxPositioner = Api.Instance.PositionerApi.CreatePositioner(positionerOptions);
        boxPositioner.OnPositionerPositionChangedDelegate += OnPositionerPositionUpdated;
    }

    private void OnPositionerPositionUpdated()
    {
        print("OnPositionerPositionUpdated ENTER");
        print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);

        var boxLocation = new LatLongAltitude();

        print("BoxLocation: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);

       // boxPositioner.

        if (boxPositioner.TryGetLatLongAltitude(out boxLocation))
        {
            box.position = Api.Instance.SpacesApi.GeographicToWorldPoint(boxLocation);
            //avatar.transform.position = Api.Instance.SpacesApi.GeographicToWorldPoint(boxLocation); //works in game view but in scence view skinks through the floor because there is no cube under him, but in the main 3DMap scene he doesnt need a cube and doesnt sink through floor so odd. something happens with the positioner. With the Geographic Transofmr he skins through even the cube when the cube is positioned!
        }

        var destLocation = LatLong.FromDegrees(Input.location.lastData.latitude, Input.location.lastData.longitude);
        Api.Instance.CameraApi.AnimateTo(destLocation, distanceFromInterest: 500, headingDegrees: 270, tiltDegrees: 30, transitionDuration: 5, jumpIfFarAway: false);
    }


    private void OnDisable()
    {
        boxPositioner.OnPositionerPositionChangedDelegate -= OnPositionerPositionUpdated;
        boxPositioner.Discard();
    }
}

