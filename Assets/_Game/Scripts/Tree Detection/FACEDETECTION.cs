using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;
public class FACEDETECTION : MonoBehaviour
{
    WebCamTexture _webCamTexture;
    CascadeClassifier cascade;
    OpenCvSharp.Rect FaceRect;
    // Start is called before the first frame update
    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        _webCamTexture = new WebCamTexture(devices[0].name);
        _webCamTexture.Play();
        cascade = new CascadeClassifier(Application.dataPath + @"haarcascade_frontalface_default.xml");


    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.mainTexture = _webCamTexture;
        Mat currentTextureforFrame = OpenCvSharp.Unity.TextureToMat(_webCamTexture);
        findNewFace(currentTextureforFrame);
        display(currentTextureforFrame);
    }

    void findNewFace(Mat currentTextureforFrame)
    {
        var faces = cascade.DetectMultiScale(currentTextureforFrame, 1.1, 2, HaarDetectionType.ScaleImage);
        if (faces.Length > 0)
        {
            Debug.Log(faces[0].Location);
            FaceRect = faces[0];
        }
    }
    void display(Mat currentTextureforFrame)
    {
        if(FaceRect != null)
        {
            currentTextureforFrame.Rectangle(FaceRect, new Scalar(250, 0, 0), 2);

        }
        Texture newtexture = OpenCvSharp.Unity.MatToTexture(currentTextureforFrame);
        GetComponent<Renderer>().material.mainTexture = newtexture;
    }
}
