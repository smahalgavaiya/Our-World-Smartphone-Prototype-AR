using UnityEngine;

public class AvatarInfoManager : MonoBehaviour
{
    private string _avatarName;
    private string _avatarLevel;
    public string _jwtToken;
    public string _walletAddress;
    public string _avatarId;


    public static AvatarInfoManager Instance = null;
    private void Start()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public string WalletAddress
    {
        get
        {
            return _walletAddress;
        }
    }

    public string AvatarId
    {
        get
        {
            return _avatarId;
        }
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

    public string JwtToken
    {
        get
        {
            return _jwtToken;
        }
    }

    public void SetAvatarNameAndLevel(string name, string level,string jwtToken,string avatarId)
    {
        _avatarName = name;
        _avatarLevel = level;
        _jwtToken = jwtToken;
        _avatarId = avatarId;
    }

    public void SetAvatarWalletAddress(string walletAddress)
    {
        _walletAddress = walletAddress;
    }
}
