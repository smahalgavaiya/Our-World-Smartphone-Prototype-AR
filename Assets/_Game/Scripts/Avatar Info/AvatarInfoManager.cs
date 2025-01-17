using SimpleJSON;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class AvatarInfoManager : MonoBehaviour
{
    private const string OASIS_ADD_KARMA = "https://api.oasisplatform.world/api/avatar/add-karma-to-avatar/";
    private const string OASIS_REMOVE_KARMA = "https://api.oasisplatform.world/api/avatar/remove-karma-from-avatar/";
    private const string OASIS_GET_AVATAR_DETAIL_BY_ID = "https://api.oasisplatform.world/api/avatar/get-avatar-detail-by-id/";

    private string _avatarName;
    private string _avatarLevel;
    public string _jwtToken;
    public string _avatarId;

    public int KarmaPoints;
    public int HealthPoints;
    public int ManaPoints;

    private struct AddKarmaData
    {
        public string karmaType;
        public string karmaSourceType;
        public string karamSourceTitle;
        public string karmaSourceDesc;
    }

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

    public string JwtToken
    {
        get
        {
            return _jwtToken;
        }
    }

    public void SetAvatarNameAndLevel(string name, string level, string jwtToken, string avatarId)
    {
        _avatarName = name;
        _avatarLevel = level;
        _jwtToken = jwtToken;
        _avatarId = avatarId;

        PlayerPrefs.SetString("AvatarName", name);
        PlayerPrefs.SetString("AvatarLevel", level);
        PlayerPrefs.SetString("AvatarId", avatarId);

        //fetching avatar details 
        getAvatarDetailsById();
    }

    //Get avatar details by id
    public void getAvatarDetailsById()
    => CheckInformationgetAvatarDetailsById();
    private void CheckInformationgetAvatarDetailsById()
    {
        _avatarId = PlayerPrefs.GetString("AvatarId");
        _jwtToken = PlayerPrefs.GetString("JWTToken");
        StartCoroutine(getAvatarDetailsByIdRequest());
       // RemoveKarma("DropLitter", "AndroidApp", "SEEDS", "SEEDS");
    }
    private IEnumerator getAvatarDetailsByIdRequest()
    {
        using var request = new UnityWebRequest(OASIS_GET_AVATAR_DETAIL_BY_ID + _avatarId);
        request.method = UnityWebRequest.kHttpVerbGET;
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", $"Bearer {PlayerPrefs.GetString("JWTToken")}");
        // ShowWarningInfoPanel(true);
        yield return request.SendWebRequest();

        JSONNode data = JSON.Parse(request.downloadHandler.text);
        Debug.Log(data);
        //write stuffs to show results over unity UI
        if (data["isError"] != null&& data["isError"].Value == "true")
            // SetInfo(ShowWarning.SignInFail, data["message"].Value);
            Debug.Log(data["message"].Value);
        else
        {
            data = data["result"];
            KarmaPoints = int.Parse(data["result"]["karma"]);
            HealthPoints = int.Parse(data["result"]["stats"]["hp"]);
            ManaPoints = int.Parse(data["result"]["stats"]["mana"]);

            PlayerPrefs.SetInt("KarmaPoints", KarmaPoints);
            PlayerPrefs.SetInt("HealthPoints", HealthPoints);
            PlayerPrefs.SetInt("ManaPoints", ManaPoints);
        }
    }


    //AddKarma
    public void AddKarma(string KarmaType,string karmaSourceType
        ,string KaramSourceTitle,string KarmaSourceDesc) 
        => CheckInformationAddKarma(KarmaType,karmaSourceType
        , KaramSourceTitle,KarmaSourceDesc);
    private void CheckInformationAddKarma(string KarmaType, string karmaSourceType
        , string KaramSourceTitle, string KarmaSourceDesc)
    {
        StartCoroutine(AddKarmaRequest(KarmaType, karmaSourceType
        , KaramSourceTitle, KarmaSourceDesc));
    }
    private IEnumerator AddKarmaRequest(string KarmaType, string karmaSourceType
        , string KaramSourceTitle, string KarmaSourceDesc)
    {
        byte[] authenticateData = GetAddKarmaDataJsonByte(KarmaType, karmaSourceType
        , KaramSourceTitle, KarmaSourceDesc);
        using var request = new UnityWebRequest(OASIS_ADD_KARMA+_avatarId);
        request.method = UnityWebRequest.kHttpVerbPOST;
        request.uploadHandler = new UploadHandlerRaw(authenticateData);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", $"Bearer {PlayerPrefs.GetString("JWTToken")}");
        // ShowWarningInfoPanel(true);
        yield return request.SendWebRequest();

        JSONNode data = JSON.Parse(request.downloadHandler.text);
        if (data["isError"] != null && data["isError"].Value == "true")
            // SetInfo(ShowWarning.SignInFail, data["message"].Value);
            Debug.Log(data["message"].Value);
        else
        {
            data = data["result"];
            KarmaPoints = int.Parse(data["result"]["totalKarma"].Value);
            Debug.Log("Karma points:" + KarmaPoints);
            PlayerPrefs.SetInt("KarmaPoints", KarmaPoints);
        }
    }
    private byte[] GetAddKarmaDataJsonByte(string _KarmaType, string karmaSourceType
        , string KaramSourceTitle, string KarmaSourceDesc)
    {
        var AddKarmaData = new AddKarmaData
        {
             karmaType=_KarmaType,
             karmaSourceType=karmaSourceType,
             karamSourceTitle = KaramSourceTitle,
             karmaSourceDesc= KarmaSourceDesc
    };
        return System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(AddKarmaData));
    }

    public void RemoveKarma(string KarmaType, string karmaSourceType
        , string KaramSourceTitle, string KarmaSourceDesc)
     => CheckInformationRemoveKarma(KarmaType, karmaSourceType
        , KaramSourceTitle, KarmaSourceDesc);

    private void CheckInformationRemoveKarma(string KarmaType, string karmaSourceType
      , string KaramSourceTitle, string KarmaSourceDesc)
    {
        StartCoroutine(RemoveKarmaRequest(KarmaType, karmaSourceType
        , KaramSourceTitle, KarmaSourceDesc));
    }
    private IEnumerator RemoveKarmaRequest(string KarmaType, string karmaSourceType
        , string KaramSourceTitle, string KarmaSourceDesc)
    {
        byte[] authenticateData = GetRequestKarmaDataJsonByte(KarmaType, karmaSourceType
        , KaramSourceTitle, KarmaSourceDesc);
        using var request = new UnityWebRequest(OASIS_REMOVE_KARMA + _avatarId);
        request.method = UnityWebRequest.kHttpVerbPOST;
        request.uploadHandler = new UploadHandlerRaw(authenticateData);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", $"Bearer {PlayerPrefs.GetString("JWTToken")}");

        // ShowWarningInfoPanel(true);
        yield return request.SendWebRequest();

        JSONNode data = JSON.Parse(request.downloadHandler.text);
        if (data["isError"] != null && data["isError"].Value == "true")
            // SetInfo(ShowWarning.SignInFail, data["message"].Value);
            Debug.Log(data["message"].Value);
        else
        {
            data = data["result"];
            KarmaPoints = int.Parse(data["result"]["totalKarma"]);
            PlayerPrefs.SetInt("KarmaPoints", KarmaPoints);
        }
    }
    private byte[] GetRequestKarmaDataJsonByte(string _KarmaType, string karmaSourceType
        , string KaramSourceTitle, string KarmaSourceDesc)
    {
        var AddKarmaData = new AddKarmaData
        {
            karmaType = _KarmaType,
            karmaSourceType = karmaSourceType,
            karamSourceTitle = KaramSourceTitle,
            karmaSourceDesc = KarmaSourceDesc
        };
        return System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(AddKarmaData));
    }
}
