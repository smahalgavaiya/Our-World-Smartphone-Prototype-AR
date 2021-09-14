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
        _avatarName.text = AvatarInfoManager.Instance.AvatarName;
        _avatarLevel.text = AvatarInfoManager.Instance.AvatarLevel;
    }
}
