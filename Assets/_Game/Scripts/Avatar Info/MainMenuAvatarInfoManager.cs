using TMPro;
using UnityEngine;

public class MainMenuAvatarInfoManager : MonoBehaviour
{
    public TextMeshProUGUI _avatarName;
    public TextMeshProUGUI _avatarLevel;
    private void Start()
    {
        UpdateDetails();
    }

    private void UpdateDetails()
    {
        _avatarName.text = PlayerPrefs.GetString("AvatarName");
        _avatarLevel.text = PlayerPrefs.GetString("AvatarLevel");
    }
}
