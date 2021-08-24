
using UnityEngine;
//using NextGenSoftware.OASIS.API.Core;
//using NextGenSoftware.OASIS.API.Providers.HoloOASIS.Unity;
//using System.Threading.Tasks;
//using TMPro;
//using NextGenSoftware.Holochain.HoloNET.Client.Unity;
//using NextGenSoftware.Holochain.HoloNET.Client.Core;
//using Newtonsoft.Json;

public class OASISAvatarManager : MonoBehaviour
{
    //private TaskCompletionSource<string> _taskCompletionSourceGetInstance = new TaskCompletionSource<string>();

    //ProfileManager ProfileManager { get; set; }  //If the ProfileManager is going to contain additional business logic not contained in the providers then use this.
    public GameObject ProfileUsername;
    public GameObject ProfileFullName;
    public GameObject ProfileDOB;
    public GameObject ProfileEmail;
    public GameObject ProfileAddress;
    public GameObject ProfileKarma;
    public GameObject ProfileLevel;

    //async Task Start()
    void Start()
    {

        /*
        // Inject in the HoloOASIS Storage Provider (this could be moved to a config file later so the 
        // providers can be sweapped without having to re-compile.
        ProfileManager = new ProfileManager(new HoloOASIS("ws://localhost:8888"));
        ProfileManager.OnProfileManagerError += ProfileManager_OnProfileManagerError;
        ProfileManager.OASISStorageProvider.OnStorageProviderError += OASISStorageProvider_OnStorageProviderError;

        //StorageProvider = new HoloOASIS("ws://localhost:8888");

        await LoadProfile();    

        */


        /*
        HoloNETClient holoNETClient = new HoloNETClient("ws://localhost:8888");
        holoNETClient.OnZomeFunctionCallBack += HoloNETClient_OnZomeFunctionCallBack;
        holoNETClient.OnConnected += HoloNETClient_OnConnected;
        holoNETClient.OnError += HoloNETClient_OnError;
        holoNETClient.OnDataReceived += HoloNETClient_OnDataReceived;
        holoNETClient.OnGetInstancesCallBack += HoloNETClient_OnGetInstancesCallBack;

        await holoNETClient.Connect();

        await holoNETClient.GetHolochainInstancesAsync();
        await _taskCompletionSourceGetInstance.Task;

        await holoNETClient.CallZomeFunctionAsync("test-instance", "our_world_core", "load_profile", new { address = "QmVtt5dEZEyTUioyh59XfFc3KWuaifK92Mc2KTXGauSbS9" });
        */
    }

    /*
    private void HoloNETClient_OnGetInstancesCallBack(object sender, GetInstancesCallBackEventArgs e)
    {
        _taskCompletionSourceGetInstance.SetResult(e.Instances[0]);
    }

    private void HoloNETClient_OnDataReceived(object sender, DataReceivedEventArgs e)
    {
        Debug.Log(string.Concat("Data Received: EndPoint: ", e.EndPoint, "RawJSONData: ", e.RawJSONData));
    }

    private void HoloNETClient_OnError(object sender, HoloNETErrorEventArgs e)
    {
        Debug.Log(string.Concat("Error Occured. Resason: ", e.Reason, ", EndPoint: ", e.EndPoint, ", Details: ", e.ErrorDetails.ToString()));
    }

    private void HoloNETClient_OnConnected(object sender, ConnectedEventArgs e)
    {
        Debug.Log("Connected to Holochain Conductor: " + e.EndPoint);
    }

    private void HoloNETClient_OnZomeFunctionCallBack(object sender, ZomeFunctionCallBackEventArgs e)
    {
        Debug.Log(string.Concat("ZomeFunction CallBack: EndPoint: ", e.EndPoint, ", Id: ", e.Id, ", Instance: ", e.Instance, ", Zome: ", e.Zome, ", ZomeFunction: ", e.ZomeFunction, ", Data: ", e.ZomeReturnData, ", Raw Zome Return Data: ", e.RawZomeReturnData, ", Raw JSON Data: ", e.RawJSONData, ", IsCallSuccessful: ", e.IsCallSuccessful ? "true" : "false"));

        Profile profile = JsonConvert.DeserializeObject<Profile>(string.Concat("{", e.ZomeReturnData, "}"));

        if (profile != null)
        {
            (ProfileFullName.GetComponent<TextMeshProUGUI>()).text = string.Concat(profile.Title, " ", profile.FirstName, " ", profile.LastName);
            (ProfileUsername.GetComponent<TextMeshProUGUI>()).text = profile.Username;
            (ProfileDOB.GetComponent<TextMeshProUGUI>()).text = profile.DOB;
            (ProfileEmail.GetComponent<TextMeshProUGUI>()).text = profile.Email;
            //(ProfileAddress.GetComponent<TextMeshProUGUI>()).text = profile.PlayerAddress;
            (ProfileKarma.GetComponent<TextMeshProUGUI>()).text = profile.Karma.ToString();
            (ProfileLevel.GetComponent<TextMeshProUGUI>()).text = profile.Level.ToString();
        }
    }
    private async Task LoadProfile()
    {
        //IProfile profile = await ProfileManager.LoadProfileAsync("dellams", "1234");
        IProfile profile = await ProfileManager.LoadProfileAsync("QmVtt5dEZEyTUioyh59XfFc3KWuaifK92Mc2KTXGauSbS9");

        if (profile != null)
        {
            (ProfileFullName.GetComponent<TextMeshProUGUI>()).text = string.Concat(profile.Title, " ", profile.FirstName, " ", profile.LastName);
            (ProfileUsername.GetComponent<TextMeshProUGUI>()).text = profile.Username;
            (ProfileDOB.GetComponent<TextMeshProUGUI>()).text = profile.DOB;
            (ProfileEmail.GetComponent<TextMeshProUGUI>()).text = profile.Email;
            //(ProfileAddress.GetComponent<TextMeshProUGUI>()).text = profile.PlayerAddress;
            (ProfileKarma.GetComponent<TextMeshProUGUI>()).text = profile.Karma.ToString();
            (ProfileLevel.GetComponent<TextMeshProUGUI>()).text = profile.Level.ToString();
        }
    }

    private void OASISStorageProvider_OnStorageProviderError(object sender, ProfileManagerErrorEventArgs e)
    {
        Debug.Log("Error occured in the OASIS Storage Provider: " + e.Reason + ", Error Details: " + e.ErrorDetails);
    }

    private void ProfileManager_OnProfileManagerError(object sender, ProfileManagerErrorEventArgs e)
    {
        Debug.Log("Error occured in the OASIS Profile Manager: " + e.Reason + ", Error Details: " + e.ErrorDetails);
    }
    */

    // Update is called once per frame
    void Update ()
    {
		
	}
}
