using System;
using System.Collections;
using UMA.CharacterSystem;
using UnityEngine;
using UnityEngine.UI;
using Wrld;
using Wrld.Common.Maths;
using Wrld.Space;
using Wrld.Space.Positioners;

public class MainMapManager : MonoBehaviour 
{
    private bool _locationReady = false;

    static LatLong LondonPosition = LatLong.FromDegrees(51.50986, -0.11809);
    static LatLong NewYorkPosition = LatLong.FromDegrees(40.730610, -73.935242);
    static LatLong SydneyPosition = LatLong.FromDegrees(-33.865143, 151.209900);
    static LatLong DubaiPosition = LatLong.FromDegrees(25.2048, 55.2708);
    static LatLong TokyoPosition = LatLong.FromDegrees(35.6762, 139.6503);
    static LatLong LisbonPosition = LatLong.FromDegrees(38.736946, -9.142685);
    static LatLong SanFranPosition = LatLong.FromDegrees(37.773972, -122.431297);

    static LatLong targetPosition = LatLong.FromDegrees(51.50986, -0.11809); //london.
    static LatLong previousPosition = LatLong.FromDegrees(37.783372, -122.400834);

    public InputField InputFieldLat;
    public InputField InputFieldLong;
    public InputField InputFieldDistanceFromInterest;
    public InputField InputFieldHeading;
    public InputField InputFieldTilt;
    public InputField InputFieldSearch;
    public Text TextLocation;
    public Text TextLogOutput;
    public Button ButtonMoveCamera;
    public Button ButtonMoveAvatar;
    public Button ButtonSearch;
    public Button ButtonFindMe;
    public Button ButtonImageFindMe;
    public Button ButtonChangeMapSystem;
    public Dropdown dropdownLocation;
    public Transform box;
    public DynamicCharacterAvatar avatar;
    public DynamicCharacterAvatar avatarOnBox;
    public Positioner boxPositioner;
    public GeographicTransform geographicTransform;
    public Transform GeoBox;
    public Transform GeoBox2;
    public WrldMap map;
    


    // Use this for initialization
    IEnumerator Start()
    {
        ButtonMoveCamera.GetComponent<Button>().onClick.AddListener(MoveCamera);
        ButtonMoveAvatar.GetComponent<Button>().onClick.AddListener(MoveAvatar);
        ButtonSearch.GetComponent<Button>().onClick.AddListener(Search);
        ButtonFindMe.GetComponent<Button>().onClick.AddListener(FindMe);
        ButtonImageFindMe.GetComponent<Button>().onClick.AddListener(FindMe);
        dropdownLocation.GetComponent<Dropdown>().onValueChanged.AddListener(delegate { LocationDropdownChanged(dropdownLocation); });

        //ButtonChangeMapSystem.GetComponent<Button>().onClick.AddListener(ChangeMapSystem);

        box.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
       // yield break;

        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            Log("GPS NOT ENABLED");
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
            Log("Timed out");
            TextLocation.GetComponent<Text>().text = "Timed out";
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Log("Unable to determine device location");
            yield break;
        }
        else
        {
            Input.compass.enabled = true;

            if (!Input.compass.enabled)
                Log("Compass disabled");
            else
                Log("Compass enabled");

            Log("INIT " + GetLocation());
            TextLocation.GetComponent<Text>().text = GetLocation();

            // LIVE ANDROID GPS
            previousPosition = LatLong.FromDegrees(-10, -10);
            _locationReady = true;
            //targetPosition.SetLatitude(Input.location.lastData.latitude);
            //  targetPosition.SetLatitude(Input.location.lastData.longitude);
        }

        // Stop service if there is no need to query location updates continuously
        //Input.location.Stop();


        // GeographicTransform transform = new GeographicTransform();

        // geographicTransform = new GeographicTransform();
        // Api.Instance.GeographicApi.RegisterGeographicTransform(geographicTransform);

    }

    //private void LocationDropdownChanged(int arg0)
    //{
    //    throw new NotImplementedException();
    //}

    // Update is called once per frame
    void Update () 
    {
		
	}

    private void OnEnable()
    {
        targetPosition = LatLong.FromDegrees(51.50986, -0.11809); //london.
        previousPosition = LatLong.FromDegrees(37.783372, -122.400834);

        var positionerOptions = new PositionerOptions()
                                                .ElevationAboveGround(5) //88 
                                                .LatitudeDegrees(targetPosition.GetLatitude())
                                                .LongitudeDegrees(targetPosition.GetLongitude());

        boxPositioner = Api.Instance.PositionerApi.CreatePositioner(positionerOptions);
        boxPositioner.OnPositionerPositionChangedDelegate += OnPositionerPositionUpdated;
    }

    private string GetLocation()
    {
        if (Input.location.isEnabledByUser)
            return "GPS Location: Lat: " + Input.location.lastData.latitude + " Long: " + Input.location.lastData.longitude + " Altitute: " + Input.location.lastData.altitude + " HorrizontalAccuracy: " + Input.location.lastData.horizontalAccuracy + " Timestamp: " + Input.location.lastData.timestamp + " trueHeading: " + Input.compass.trueHeading + " magneticHeading: " + Input.compass.magneticHeading + " rawVector: " + Input.compass.rawVector;
        else
            return "GPS DISABLED";
    }

    private void OnPositionerPositionUpdated()
    {
        double posLat = 0;
        double posLong = 0;

        // Give the GPS more time to come online...
        
        if (Input.location.isEnabledByUser && !_locationReady)
            return;

        var boxLocation = new LatLongAltitude();
        TextLocation.GetComponent<Text>().text = GetLocation();

        // LIVE ANDROID GPS
        // previousPosition = LatLong.FromDegrees(-10, -10);

        
        if (Input.location.isEnabledByUser && _locationReady)
        {
            avatarOnBox.transform.rotation = Quaternion.Euler(0, -Input.compass.trueHeading, 0);

            if (targetPosition.GetLatitude() != Input.location.lastData.latitude && targetPosition.GetLongitude() != Input.location.lastData.longitude)
            {
                Log("Setting Lat: " + Input.location.lastData.latitude + ", Lomg: " + Input.location.lastData.longitude);
                targetPosition.SetLatitude(Input.location.lastData.latitude);
                targetPosition.SetLatitude(Input.location.lastData.longitude);

                posLat = Input.location.lastData.latitude;
                posLong = Input.location.lastData.longitude;

                Log("Lat now: " + targetPosition.GetLatitude() + ", Long: " + targetPosition.GetLongitude());
                Log("posLat = " + posLat);
                Log("posLong = " + posLong);
            }
        }

        if (targetPosition.GetLatitude() != previousPosition.GetLatitude() && targetPosition.GetLongitude() != previousPosition.GetLongitude())
        {
            Log("POS CHANGED SO UPDATING AVATAR & CAMERA");
            Log(GetLocation());
            TextLocation.GetComponent<Text>().text = GetLocation();

            if (boxPositioner.TryGetLatLongAltitude(out boxLocation))
            {
                DoubleVector3 ECEFLocation;
                Vector3 ScreenPoint;
                LatLongAltitude test3;

                boxPositioner.TryGetECEFLocation(out ECEFLocation);
                boxPositioner.TryGetScreenPoint(out ScreenPoint);

                

                Log("ECEFLocation x: " + ECEFLocation.x + ", y: " + ECEFLocation.y + ECEFLocation.z + "...");
                Log("ScreenPoint x: " + ScreenPoint.x + ", y: " + ScreenPoint.y + ScreenPoint.z + "...");
                Log("Moving Avatar To Lat: " + boxLocation.GetLatitude() + ", Long: " + boxLocation.GetLongitude() + "...");
                
                box.position = Api.Instance.SpacesApi.GeographicToWorldPoint(boxLocation);
                Log("box.position : x: " + box.position.x + ", y: " + box.position.y + ",z=" + box.position.z + "...");


                //geographicTransform.UpdateTransform(ITransformUpdateStrategy )


              //  GeographicTransform geotest = new GeographicTransform();
          //      geotest.SetPosition(targetPosition);

                //If you try to use the GeographicTransform you will get a null exception because the internal Positioner it uses has not been instantted.
              //  geographicTransform.SetPosition(targetPosition);
                //Log("geographicTransform pos = Lat: " + geographicTransform.GetPosition().GetLatitude() + " long: " + geographicTransform.GetPosition().GetLongitude());

                //geographicTransform.TryGetLatLongAltitude(out test3);
                //Log("geographicTransform Lat: " + test3.GetLatitude() + ", Long: " + test3.GetLongitude() + "...");
                
                //GeoBox.position = Api.Instance.SpacesApi.GeographicToWorldPoint(test3);
                //Log("GeoBox.position : x: " + GeoBox.position.x + ", y: " + GeoBox.position.y + ",z=" + GeoBox.position.z + "...");

                //GeoBox2.position = Api.Instance.SpacesApi.GeographicToWorldPoint(test3);
                //Log("GeoBox.position : x: " + GeoBox2.position.x + ", y: " + GeoBox2.position.y + ",z=" + GeoBox2.position.z + "...");



                //avatar.transform.position = Api.Instance.SpacesApi.GeographicToWorldPoint(boxLocation); //works in game view but in scence view skinks through the floor because there is no cube under him, but in the main 3DMap scene he doesnt need a cube and doesnt sink through floor so odd. something happens with the positioner. With the Geographic Transofmr he skins through even the cube when the cube is positioned!
                //avatar.transform.rotation = Quaternion.Euler(0, -Input.compass.trueHeading, 0);
            }

            //var destLocation = LatLong.FromDegrees(targetPosition.GetLatitude(), targetPosition.GetLongitude());
            var destLocation = LatLong.FromDegrees(targetPosition.GetLatitude(), targetPosition.GetLongitude());

            LatLong testLocation = new LatLong(targetPosition.GetLatitude(), targetPosition.GetLongitude());

            Log("targetPosition = Lat: " + targetPosition.GetLatitude() + ", Long: " + targetPosition.GetLongitude());
            Log("destLocation = Lat: " + destLocation.GetLatitude() + ", Long: " + destLocation.GetLongitude());
            Log("testLocation = Lat: " + testLocation.GetLatitude() + ", Long: " + testLocation.GetLongitude());

            //Api.Instance.CameraApi.AnimateTo(destLocation, distanceFromInterest: 500, headingDegrees: 270, tiltDegrees: 30, transitionDuration: 5, jumpIfFarAway: false);

            LatLong tar = new LatLong(posLat, posLong);
            Log("Moving Camera To Lat: " + tar.GetLatitude() + ", Long: " + tar.GetLongitude() + "...");
            
            // ZOOM TO GPS LOCATION (Currently crashes into the sea! lol I beleive because need to take account of the curvature of the earth...)
            //Api.Instance.CameraApi.AnimateTo(tar, distanceFromInterest: 1, headingDegrees: 270, tiltDegrees: 120, transitionDuration: 5, jumpIfFarAway: false);
            
            //Temp just zoom to fixed location in London (with avatar standing on the bridge).
            Api.Instance.CameraApi.AnimateTo(LondonPosition, distanceFromInterest: 1, headingDegrees: 270, tiltDegrees: 120, transitionDuration: 5, jumpIfFarAway: false);

           // avatar.transform.position = Api.Instance.SpacesApi.GeographicToWorldPoint(LondonPosition); //works in game view but in scence view skinks through the floor because there is no cube under him, but in the main 3DMap scene he doesnt need a cube and doesnt sink through floor so odd. something happens with the positioner. With the Geographic Transofmr he skins through even the cube when the cube is positioned!
         //   box.transform.position = Api.Instance.SpacesApi.GeographicToWorldPoint(LondonPosition); //works in game view but in scence view skinks through the floor because there is no cube under him, but in the main 3DMap scene he doesnt need a cube and doesnt sink through floor so odd. something happens with the positioner. With the Geographic Transofmr he skins through even the cube when the cube is positioned!

            //  avatar.transform.rotation = Quaternion.Euler(0, -Input.compass.trueHeading, 0);


            //Log("Moving Camera To Lat: " + targetPosition.GetLatitude() + ", Long: " + targetPosition.GetLongitude() + "...");
            //Api.Instance.CameraApi.AnimateTo(targetPosition, distanceFromInterest: 1, headingDegrees: 270, tiltDegrees: 120, transitionDuration: 5, jumpIfFarAway: false);
            previousPosition.SetLatitude(targetPosition.GetLatitude());
            previousPosition.SetLongitude(targetPosition.GetLongitude());
        }
        //else
          //  Log("POS NOT CHANGED");

    }


    private void OnDisable()
    {
        boxPositioner.OnPositionerPositionChangedDelegate -= OnPositionerPositionUpdated;
        boxPositioner.Discard();
    }

    private void MoveCamera()
    {
        Log("text = " + InputFieldLat.GetComponent<GUIText>().text);
        //Log("text = " + InputFieldLat.GetComponent<Text>().text);

        //var startLocation = LatLong.FromDegrees(37.7858, -122.401);
        //var startLocation = LatLong.FromDegrees(double.Parse(InputFieldLat.GetComponent<Text>().text), double.Parse(InputFieldLong.GetComponent<Text>().text));

       // Log("Moving Camera To Lat: " + startLocation.GetLatitude() + ", Long: " + startLocation.GetLongitude() + "...");
        //Api.Instance.CameraApi.AnimateTo(startLocation, distanceFromInterest: double.Parse(InputFieldDistanceFromInterest.GetComponent<Text>().text), headingDegrees: double.Parse(InputFieldHeading.GetComponent<Text>().text), tiltDegrees: double.Parse(InputFieldTilt.GetComponent<Text>().text));
    }

    private void MoveAvatar()
    {
        Log("text = " + InputFieldLat.GetComponent<Text>().text);

        //Mock the LatLong when in Desktop/Editor Mode.
        //targetPosition.SetLatitude(double.Parse(InputFieldLat.GetComponent<Text>().text));
        //targetPosition.SetLatitude(double.Parse(InputFieldLong.GetComponent<Text>().text));

        //boxPositioner.SetPosition(targetPosition);
    }

    private void ChangeMapSystem()
    {
       // map.sp
    }

    private void FindMe()
    {
        Api.Instance.CameraApi.AnimateTo(LondonPosition, distanceFromInterest: 1, headingDegrees: 270, tiltDegrees: 120, transitionDuration: 5, jumpIfFarAway: false);
    }

    private void LocationDropdownChanged(Dropdown change)
    {
        switch (change.value)
        {
            case 0:
                Api.Instance.CameraApi.AnimateTo(LondonPosition, distanceFromInterest: 1, headingDegrees: 270, tiltDegrees: 120, transitionDuration: 5, jumpIfFarAway: false);
                break;

            case 1:
                Api.Instance.CameraApi.AnimateTo(NewYorkPosition, distanceFromInterest: 1, headingDegrees: 270, tiltDegrees: 120, transitionDuration: 5, jumpIfFarAway: false);
                break;

            case 3:
                Api.Instance.CameraApi.AnimateTo(SydneyPosition, distanceFromInterest: 1, headingDegrees: 270, tiltDegrees: 120, transitionDuration: 5, jumpIfFarAway: false);
                break;

            case 4:
                Api.Instance.CameraApi.AnimateTo(DubaiPosition, distanceFromInterest: 1, headingDegrees: 270, tiltDegrees: 120, transitionDuration: 5, jumpIfFarAway: false);
                break;

            case 5:
                Api.Instance.CameraApi.AnimateTo(TokyoPosition, distanceFromInterest: 1, headingDegrees: 270, tiltDegrees: 120, transitionDuration: 5, jumpIfFarAway: false);
                break;

            case 6:
                Api.Instance.CameraApi.AnimateTo(LisbonPosition, distanceFromInterest: 1, headingDegrees: 270, tiltDegrees: 120, transitionDuration: 5, jumpIfFarAway: false);
                break;

            case 7:
                Api.Instance.CameraApi.AnimateTo(SanFranPosition, distanceFromInterest: 1, headingDegrees: 270, tiltDegrees: 120, transitionDuration: 5, jumpIfFarAway: false);
                break;
        }
    }

    private void Search()
    {
        string search = InputFieldLat.GetComponent<Text>().text;
        LatLong searchLocation;

        if (search.Contains(","))
        {
            string[] parts = search.Split(',');
            searchLocation = LatLong.FromDegrees(double.Parse(parts[0]), double.Parse(parts[1]));
        }
        else
        {
            //Do reverse-geolocation lookup
            searchLocation = LatLong.FromDegrees(double.Parse(InputFieldLat.GetComponent<Text>().text), double.Parse(InputFieldLong.GetComponent<Text>().text));
        }

        Log("Moving Camera To Lat: " + searchLocation.GetLatitude() + ", Long: " + searchLocation.GetLongitude() + "...");
        Api.Instance.CameraApi.AnimateTo(searchLocation, distanceFromInterest: double.Parse(InputFieldDistanceFromInterest.GetComponent<Text>().text), headingDegrees: double.Parse(InputFieldHeading.GetComponent<Text>().text), tiltDegrees: double.Parse(InputFieldTilt.GetComponent<Text>().text));
    }

    private void Log(string message)
    {
        TextLocation.GetComponent<Text>().text += message + "\\n";
        // print(message);
         Debug.Log(message);

    }
}
