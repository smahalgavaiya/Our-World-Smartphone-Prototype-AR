using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject fullBodyView;
    public GameObject headView;
    public GameObject faceView;
    public GameObject upperBodyView;
    public GameObject lowerBodyView;

    public GameObject fullBodyViewF;
    public GameObject headViewF;
    public GameObject faceViewF;
    public GameObject upperBodyViewF;
    public GameObject lowerBodyViewF;

    public bool fullBodyBool,headViewBool,faceViewBool,upperBodyBool,lowerBodyBool,genderMale;

    public Vector3 currentPos;
    public Vector3 newPos;

    public float runTime,TimetoMove;
    public Quaternion currentRot;
    public Quaternion newRot;
    // Start is called before the first frame update
    void Start()
    {
         mainCamera.gameObject.transform.position = fullBodyView.gameObject.transform.position;
         mainCamera.gameObject.transform.rotation = fullBodyView.gameObject.transform.rotation;
    }

    public void FocusFullbodyView()
    {
        runTime = 0;
        fullBodyBool = true;
        headViewBool = false;
        faceViewBool = false;
        upperBodyBool = false;
        lowerBodyBool = false;
    }

    public void FocusHeadView()
    {
        runTime = 0;
        fullBodyBool = false;
        headViewBool = true;
        faceViewBool = false;
        upperBodyBool = false;
        lowerBodyBool = false;
    }

    public void FocusFaceView()
    {
        runTime = 0;
        fullBodyBool = false;
        headViewBool = false;
        faceViewBool = true;
        upperBodyBool = false;
        lowerBodyBool = false;
    }

    public void FocusUpperBodyView()
    {
        runTime = 0;
        fullBodyBool = false;
        headViewBool = false;
        faceViewBool = false;
        upperBodyBool = true;
        lowerBodyBool = false;
    }

    public void FocusLowerBodyView()
    {
        runTime = 0;
        fullBodyBool = false;
        headViewBool = false;
        faceViewBool = false;
        upperBodyBool = false;
        lowerBodyBool = true;
    }



    // Update is called once per frame
    void Update()
    {
        if(fullBodyBool)
        {
            runTime += Time.deltaTime;

            currentPos = mainCamera.gameObject.transform.position;
            currentRot = mainCamera.gameObject.transform.rotation;

            if(genderMale)
            {
                newPos = fullBodyView.gameObject.transform.position;
                newRot = fullBodyView.gameObject.transform.rotation;
            }
            else
            {
                newPos = fullBodyViewF.gameObject.transform.position;
                newRot = fullBodyViewF.gameObject.transform.rotation;
            }

            mainCamera.gameObject.transform.position = Vector3.Lerp(currentPos, newPos, runTime/TimetoMove);
            mainCamera.gameObject.transform.rotation = Quaternion.Lerp(currentRot, newRot, runTime/TimetoMove);

            if (mainCamera.gameObject.transform.position == newPos)
            {
                fullBodyBool = false;
            }
        }

        else if (headViewBool)
        {
            runTime += Time.deltaTime;

            currentPos = mainCamera.gameObject.transform.position;
            currentRot = mainCamera.gameObject.transform.rotation;

            if (genderMale)
            {
                newPos = headView.gameObject.transform.position;
                newRot = headView.gameObject.transform.rotation;
            }
            else
            {
                newPos = headViewF.gameObject.transform.position;
                newRot = headViewF.gameObject.transform.rotation;
            }
            mainCamera.gameObject.transform.position = Vector3.Lerp(currentPos, newPos, runTime / TimetoMove);
            mainCamera.gameObject.transform.rotation = Quaternion.Lerp(currentRot, newRot, runTime / TimetoMove);

            if (mainCamera.gameObject.transform.position == newPos)
            {
                headViewBool = false;
            }
        }

        else if (faceViewBool)
        {
            runTime += Time.deltaTime;

            currentPos = mainCamera.gameObject.transform.position;
            currentRot = mainCamera.gameObject.transform.rotation;

            if(genderMale)
            {
                newPos = faceView.gameObject.transform.position;
                newRot = faceView.gameObject.transform.rotation;
            }
            else
            {
                newPos = faceViewF.gameObject.transform.position;
                newRot = faceViewF.gameObject.transform.rotation;
            }


            mainCamera.gameObject.transform.position = Vector3.Lerp(currentPos, newPos, runTime / TimetoMove);
            mainCamera.gameObject.transform.rotation = Quaternion.Lerp(currentRot, newRot, runTime / TimetoMove);

            if (mainCamera.gameObject.transform.position == newPos)
            {
                faceViewBool = false;
            }
        }

        else if (upperBodyBool)
        {
            runTime += Time.deltaTime;

            currentPos = mainCamera.gameObject.transform.position;
            currentRot = mainCamera.gameObject.transform.rotation;

            if(genderMale)
            {
                newPos = upperBodyView.gameObject.transform.position;
                newRot = upperBodyView.gameObject.transform.rotation;
            }
            else
            {
                newPos = upperBodyViewF.gameObject.transform.position;
                newRot = upperBodyViewF.gameObject.transform.rotation;
            }

            mainCamera.gameObject.transform.position = Vector3.Lerp(currentPos, newPos, runTime / TimetoMove);
            mainCamera.gameObject.transform.rotation = Quaternion.Lerp(currentRot, newRot, runTime / TimetoMove);

            if (mainCamera.gameObject.transform.position == newPos)
            {
                upperBodyBool = false;
            }
        }

        else if (lowerBodyBool)
        {
            runTime += Time.deltaTime;

            currentPos = mainCamera.gameObject.transform.position;
            currentRot = mainCamera.gameObject.transform.rotation;

            if(genderMale)
            {
                newPos = lowerBodyView.gameObject.transform.position;
                newRot = lowerBodyView.gameObject.transform.rotation;
            }
            else
            {
                newPos = lowerBodyView.gameObject.transform.position;
                newRot = lowerBodyView.gameObject.transform.rotation;
            }

            mainCamera.gameObject.transform.position = Vector3.Lerp(currentPos, newPos, runTime / TimetoMove);
            mainCamera.gameObject.transform.rotation = Quaternion.Lerp(currentRot, newRot, runTime / TimetoMove);

            if (mainCamera.gameObject.transform.position == newPos)
            {
                lowerBodyBool = false;
            }
        }
    }
}
