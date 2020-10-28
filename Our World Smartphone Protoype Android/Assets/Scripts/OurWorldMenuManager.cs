
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Wrld;
using Wrld.Space;

public class OurWorldMenuManager : MonoBehaviour
{
    public Button ButtonSwitchToHolographicProjectionView;
   // public GameObject ButtonNewGame;
    public Button ButtonMainMenu;
    public Button ButtonManyEarths;
    public Button ButtonAvatar;
    public Button Button3DMap;
    public Button ButtonARCoreManoMotion;
    public Text txtHCOutput;
    public Text txt;
    
    // Use this for initialization
    void Start ()
    {
        ButtonSwitchToHolographicProjectionView.GetComponent<Button>().onClick.AddListener(ChangeToHolographicProjectionView);
        ButtonMainMenu.GetComponent<Button>().onClick.AddListener(ShowMainMenu);
        ButtonManyEarths.GetComponent<Button>().onClick.AddListener(ChangeToManyEarthsView);
        ButtonAvatar.GetComponent<Button>().onClick.AddListener(ChangeToAvatarView);
        Button3DMap.GetComponent<Button>().onClick.AddListener(ChangeTo3DMapView);
        ButtonARCoreManoMotion.GetComponent<Button>().onClick.AddListener(ChangeToManorMotionView);
    }

    // Update is called once per frame
    void Update () {
		
	}

    
    /*
    void ShowToast(string text, int duration)
    {
       // StartCoroutine(showToastCOR(text, duration));
    }

    private IEnumerator showToastCOR(string text,
        int duration)
    {
        Color orginalColor = txt.color;

        txt.text = text;
        txt.enabled = true;

        //Fade in
        yield return fadeInAndOut(txt, true, 0.5f);

        //Wait for the duration
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return null;
        }

        //Fade out
        yield return fadeInAndOut(txt, false, 0.5f);

        txt.enabled = false;
        txt.color = orginalColor;
    }

    IEnumerator fadeInAndOut(Text targetText, bool fadeIn, float duration)
    {
        //Set Values depending on if fadeIn or fadeOut
        float a, b;
        if (fadeIn)
        {
            a = 0f;
            b = 1f;
        }
        else
        {
            a = 1f;
            b = 0f;
        }

        Color currentColor = Color.clear;
        float counter = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(a, b, counter / duration);

            targetText.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            yield return null;
        }
    }*/

    public void ChangeTo3DMapView()
    {
        Log("Switching to 3D Map...");
        UnityEngine.SceneManagement.SceneManager.LoadScene("UnityWorldSpace");
        Log("Switching to 3D Map... Done");
    }

    public void ChangeToHolographicProjectionView()
    {
        Log("Switching to Holographic Projection View...");
        UnityEngine.SceneManagement.SceneManager.LoadScene("ARCore Sample Scene");
        Log("Switching to Holographic Projection View... Done");
    }


    //public void NewGame()
    //{
    //    UnityEngine.SceneManagement.SceneManager.LoadScene("UnityWorldSpace");
    //}

    public void ShowMainMenu()
    {
        Log("Switching to Main Menu...");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu 3D");
        Log("Switching to Main Menu... Done");
    }

    public void ChangeToManyEarthsView()
    {
        Log("Switching to Many Earths View...");
        UnityEngine.SceneManagement.SceneManager.LoadScene("ManyEarths");
        Log("Switching to Many Earths View... Done");
    }

    public void ChangeToManorMotionView()
    {
        Log("Switching to Crazy Hands...");
        UnityEngine.SceneManagement.SceneManager.LoadScene("ARCoreManoMotion");
        Log("Switching to Crazy Hands... Done!");
    }


    public void ChangeToAvatarView()
    {
        Log("Switching to Avatar View...");
        UnityEngine.SceneManagement.SceneManager.LoadScene("OASIS Avatar");
        Log("Switching to Avatar View... Done");


        //UnityEngine.SceneManagement.SceneManager.LoadScene("skyboxspaceFreeexample");

        /*
        HoloNETClient holoNETClient = new HoloNETClient("ws://localhost:8888");
        holoNETClient.OnConnected += HoloNETClient_OnConnected;
        holoNETClient.OnDataReceived += HoloNETClient_OnDataReceived;
        holoNETClient.OnZomeFunctionCallBack += HoloNETClient_OnZomeFunctionCallBack;
        holoNETClient.OnError += HoloNETClient_OnError;

        holoNETClient.Connect();
        holoNETClient.CallZomeFunctionAsync("test-instance", "our_world_core", "test", "");
        */
    }


    /*
    /// <param name="message">Message string to show in the toast.</param>
    private void _ShowAndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }
    }
    */


    /*
    private void HoloNETClient_OnError(object sender, ErrorEventArgs e)
    {
        //EditorUtility.DisplayDialog("Error Occured", "Error Occured. Resason: " + e.Reason + ", EndPoint: " + e.EndPoint + ", Details: " + e.ErrorDetails.ToString(), "OK");
        string msg = "Error Occured. Resason: " + e.Reason + ", EndPoint: " + e.EndPoint + ", Details: " + e.ErrorDetails.ToString();
        Debug.Log(msg);
        LogToScreen(msg);
    }

    private void HoloNETClient_OnZomeFunctionCallBack(object sender, ZomeFunctionCallBackEventArgs e)
    {
        // EditorUtility.DisplayDialog("Zome Function Callback", "ZomeFunctionCallback: Id: " + e.Id + ", Instance: " + e.Instance + ", ZomeFunction: " + e.ZomeFunction + ", Data: " + e.ZomeReturnData, "Ok");
        string msg = "ZomeFunctionCallback: Id: " + e.Id + ", Instance: " + e.Instance + ", ZomeFunction: " + e.ZomeFunction + ", Data: " + e.ZomeReturnData;
        Debug.Log(msg);
        LogToScreen(msg);
    }

    private void HoloNETClient_OnDataReceived(object sender, DataReceivedEventArgs e)
    {
        Debug.Log("Data Received!");
        LogToScreen("Data Received!");
    }

    private void HoloNETClient_OnConnected(object sender, ConnectedEventArgs e)
    {
        //EditorUtility.DisplayDialog("Connected!", "Connected", "Yay!");
        Debug.Log("Connected!");
        LogToScreen("Connected!");
    }
    */

    private void LogToScreen(string message)
    {
        Text text = txtHCOutput.GetComponent<Text>();
        text.text = text.text + message + "\n";
    }

    private void Log(string message)
    {
        Debug.Log(message);
    }


}
