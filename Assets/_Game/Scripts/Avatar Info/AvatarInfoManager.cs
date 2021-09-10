using UnityEngine;

public class AvatarInfoManager : MonoBehaviour
{
    private string _avatarName;
    private string _avatarLevel;

    public static AvatarInfoManager Instance = null;
    private void Start()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public string AvatarName
    {
        get
        {
            return _avatarName;
        }
    }
    public string AvatarLevel
    {
        get
        {
            return _avatarLevel;
        }
    }
    public void SetAvatarNameAndLevel(string name, string level)
    {
        _avatarName = name;
        _avatarLevel = level;
    }

}
