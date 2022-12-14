using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manageLandBuyButton : MonoBehaviour
{
    public GameObject landBuyManagerGO;
    public GameObject landBuyUiGO;
    public Button landBuyBtn;
    // Start is called before the first frame update
    void Start()
    {
        landBuyBtn.onClick.AddListener(enableOrDisableGridsPanel);
        landBuyManagerGO.SetActive(false);
        landBuyUiGO.SetActive(false);
    }

     public void enableOrDisableGridsPanel()
    {
        if (landBuyManagerGO.activeInHierarchy || landBuyUiGO.activeInHierarchy)
        {
            landBuyManagerGO.SetActive(false);
            landBuyUiGO.SetActive(false);
        }
        else 
        {
            landBuyManagerGO.SetActive(true);
            landBuyUiGO.SetActive(true);
        }
    }
}
