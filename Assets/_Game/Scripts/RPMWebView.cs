using UnityEngine;
using Wolf3D.ReadyPlayerMe.AvatarSDK;

public class RPMWebView : MonoBehaviour
{
    private Webview webview;
    private GameObject avatar;

    [SerializeField] private GameObject loadingLabel = null;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        webview = new Webview();
        webview.OnAvatarCreated = OnAvatarCreated;
        webview.SetScreenPadding(0, 0, 0, 0);
        webview.CreateWebview(this);
        webview.SetVisible(true);
    }

    private void OnAvatarCreated(string url)
    {
        if (avatar) Destroy(avatar);

        Screen.orientation = ScreenOrientation.Landscape;
        loadingLabel.SetActive(true);
        webview.SetVisible(false);

        AvatarLoader avatarLoader = new AvatarLoader();
        avatarLoader.LoadAvatar(url, OnAvatarLoaded);
    }

    private void OnAvatarLoaded(GameObject avatar, AvatarMetaData metaData)
    {
        this.avatar = avatar;
        loadingLabel.SetActive(false);
        avatar.transform.position = new Vector3(0, 0, 0);
        avatar.transform.localEulerAngles = new Vector3(0, 180, 0);
    }
}
