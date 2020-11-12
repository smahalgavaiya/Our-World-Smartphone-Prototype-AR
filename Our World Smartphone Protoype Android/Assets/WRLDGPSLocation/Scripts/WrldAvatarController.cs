using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wrld;
using Wrld.Space;
using Wrld.Space.Positioners;

public class WrldAvatarController : MonoBehaviour
{
    [SerializeField]
    private CharacterAvatarBuilder avatarBuilder;
    
    [SerializeField]
    private GeographicTransform geographicTransform;

    [SerializeField]
    private GPSLocationProvider locationProvider;

    private bool isUsingFixedLocation = true;

    void Start()
    {
        Api.Instance.OnInitialStreamingComplete += OnInitialStreamingComplete;   
    }

    void OnInitialStreamingComplete()
    {
        var avatar = avatarBuilder.BuildAvatar();
        avatar.transform.parent = geographicTransform.transform;
        avatar.transform.localPosition = new Vector3(0f, 0.5f, 0f);
    }

    public void PositionAtGPS()
    {
        if (isUsingFixedLocation && locationProvider.IsReady) 
        {
            isUsingFixedLocation = false;
            Debug.Log("PositionAtGPS");
            locationProvider.OnLocationUpdated += UpdateLocation;
        }
    }

    public void UpdateLocation(LatLong latLong)
    {
        Debug.Log("Location Updated");
        Api.Instance.CameraApi.MoveTo(latLong, distanceFromInterest: 300);

        var ray = Api.Instance.SpacesApi.LatLongToVerticallyDownRay(latLong);
        LatLongAltitude buildingIntersectionPoint;
        var didIntersectBuilding = Api.Instance.BuildingsApi.TryFindIntersectionWithBuilding(ray, out buildingIntersectionPoint);
        if (didIntersectBuilding)
        {
            Debug.Log("didIntersectBuilding");
            geographicTransform.SetPosition(buildingIntersectionPoint.GetLatLong());
            geographicTransform.SetElevation(buildingIntersectionPoint.GetAltitude());
            geographicTransform.SetElevationMode(ElevationMode.HeightAboveSeaLevel);
        }
        else
        {
            geographicTransform.SetPosition(latLong);
            geographicTransform.SetElevation(0);
            geographicTransform.SetElevationMode(ElevationMode.HeightAboveGround);
        }
    }
}
