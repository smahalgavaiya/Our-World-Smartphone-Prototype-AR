using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cameraManager : MonoBehaviour
{
    public GameObject cameraBirdEye, cameraFPP, cameraTPP,cameraCustom;

    public Dropdown cameraViewDropDown;

    public GameObject adjustcameraButton;

    public float mouseSensitivity = 2f;

    bool adjustcamera = false;
    private float yRot;
    private float xRot;
    // Start is called before the first frame update
    void Start()
    {
        cameraViewDropDown.onValueChanged.AddListener(cameraChangee);
        adjustcameraButton.GetComponent<Button>().onClick.AddListener(startcameraAdjust);
    }

    void cameraChangee(int value) 
    {
        switch (value)
        { 
            //Bird 
            case 0:
                cameraBirdEye.SetActive(true);
                cameraFPP.SetActive(false);
                cameraTPP.SetActive(false);
                cameraCustom.SetActive(false);
                break;
                //FPP
            case 1:
                cameraFPP.SetActive(true);
                cameraBirdEye.SetActive(false);
                cameraTPP.SetActive(false);
                cameraCustom.SetActive(false);
                break;
                //TPP
            case 2:
                cameraTPP.SetActive(true);
                cameraBirdEye.SetActive(false);
                cameraFPP.SetActive(false);
                cameraCustom.SetActive(false);
                break;
                //custom
            case 3:
                cameraCustom.SetActive(true);
                cameraBirdEye.SetActive(false);
                cameraFPP.SetActive(false);
                cameraTPP.SetActive(false);
                adjustcameraButton.SetActive(true);
                break;
            default:
                cameraBirdEye.SetActive(true);
                cameraFPP.SetActive(false);
                cameraTPP.SetActive(false);
                cameraCustom.SetActive(false);
                break;

        }
    }

    void startcameraAdjust()
    {
        if (adjustcameraButton.GetComponentInChildren<Text>().text != "Exit & Save")
        {
            adjustcamera = true;
            adjustcameraButton.GetComponentInChildren<Text>().text = "Exit & Save";
        }

        else
        {
            adjustcamera = false;
            adjustcameraButton.GetComponentInChildren<Text>().text = "Adjust Camera";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (adjustcamera)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (move != Vector3.zero)
            {
                cameraCustom.transform.localPosition += move;
            }
            yRot += Input.GetAxis("Mouse X") * mouseSensitivity;
            xRot -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            cameraCustom.transform.localEulerAngles = new Vector3(xRot, yRot, transform.localEulerAngles.z);

        }

    }
}
