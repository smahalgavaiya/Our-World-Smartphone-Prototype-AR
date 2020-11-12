using System.Collections;
using System.Collections.Generic;
using ParkFindingScripts.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ParkButton : MonoBehaviour
{
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Button button;

    private ParkButtonModel model;

    public void Setup(ParkButtonModel model)
    {
        nameText.text = model.ParkName;
        //TODO:Add event listener to button
    }
}
