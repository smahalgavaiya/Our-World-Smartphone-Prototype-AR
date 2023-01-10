using SimpleJSON;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class getLandPrice : MonoBehaviour
{
    private const string OASIS_GETOLAND_PRICE = "https://api.oasisplatform.world/api/Nft/GetOLANDPrice";
    public int noOfGridsSelected=0;
    public string CouponCode = "minimeu";
    string api_with_param = OASIS_GETOLAND_PRICE + "?count=0&couponCode=minimeu";


    public TMP_Text total, per_tile, address, tile_count, selection_info;

    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(getLandPriceEnum());
        per_tile.text = "$17";
    }

    public void getPrice(int count)
    {
        noOfGridsSelected = count;
        tile_count.text = count.ToString();
        api_with_param = OASIS_GETOLAND_PRICE + "?count=" + noOfGridsSelected.ToString() + "&couponCode=minimeu";
        Debug.Log(api_with_param);
        StartCoroutine(getLandPriceEnum());
    }



    private IEnumerator getLandPriceEnum()
    {
         using var request = new UnityWebRequest(api_with_param);
        request.method = UnityWebRequest.kHttpVerbGET;
        request.SetRequestHeader("Authorization", "Bearer " + AvatarInfoManager.Instance.JwtToken);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        Debug.Log(request.downloadHandler.text);
        JSONNode data = JSON.Parse(request.downloadHandler.text);
        if (data["isError"].Value == "false")
        {
            total.text = "$"+data["result"].Value;
        }
        //Debug.Log(data);
  
    }
}
