using System.Collections.Generic;
using TMPro;
using UMA;
using UMA.CharacterSystem;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreatorScript : MonoBehaviour
{
    public DynamicCharacterAvatar dynamicCharacterAvatarScript;
    public CameraController cameraControllerScript;
    public Dictionary<string, DnaSetter> dNAmodule;

    public string CharacterData;

    [Header("Male and female Race names")]

    public string maleRaceName;
    public string femaleRaceName;
    public bool genderMale;

    [Space(10)]

    [Header("DNA module related data")]

    public List<Slider> DNAModuleSliders;
    public List<Slider> DNAModuleSlidersTMP;
    public List<float> DNASliderValuesTMP;

    public List<string> DNAModuleStrings;

    public int dNAModuleValue;
    [Space(10)]

    [Header("Guise module related data")]

    public List<string> GuiseModuleStringsMale;
    public List<string> hairModuleMale;
    public List<string> beardModuleMale;
    public List<string> eyesModuleMale;
    public List<string> eyeBrowsModuleMale;
    public List<string> chestModuleMale;
    public List<string> legsModuleMale;
    public List<string> feetModuleMale;

    [Space(20)]

    public List<string> GuiseModuleStringsFemale;
    public List<string> hairModuleFemale;
    public List<string> chestModuleFemale;
    public List<string> legsModuleFemale;
    public List<string> feetModuleFemale;
    public List<string> UnderwearModuleFemale;


    public int guiseModuleValue;
    public int guiseSubModuleValue;

    public TMP_Text guiseModuleText;
    public TMP_Text guiseSubModuleText;
    public int hairModulevalueMale;
    public int beardModulevalueMale;
    public int eyesModulevalueMale;
    public int eyebrowsModulevalueMale;
    public int chestModulevalueMale;
    public int LegModulevalueMale;
    public int feetModulevalueMale;

    public int hairModulevalueFemale;
    public int underwearModulevalueFemale;
    public int chestModulevalueFemale;
    public int LegModulevalueFemale;
    public int feetModulevalueFemale;

    [Space(10)]

    [Header("Color module related data")]

    public List<string> ColorModuleStrings;
    public int ColorModuleValue;
    public TMP_Text colorModuleText;
    [Space(10)]



    #region recipies

    public List<string> chestRecipieNames;

    #endregion

    #region GenderChanges


    // changes the race of the UMA
    public void ChangeGender(bool genderState)
    {
        if (genderState && dynamicCharacterAvatarScript.activeRace.name != maleRaceName)
        {
            dynamicCharacterAvatarScript.ChangeRace(maleRaceName);
            dynamicCharacterAvatarScript.BuildCharacter();
            genderMale = true;
            cameraControllerScript.genderMale = true;
        }
        else if (!genderState && dynamicCharacterAvatarScript.activeRace.name != femaleRaceName)
        {
            dynamicCharacterAvatarScript.ChangeRace(femaleRaceName);
            dynamicCharacterAvatarScript.BuildCharacter();
            genderMale = false;
            cameraControllerScript.genderMale = false;
        }
    }

    public void RecognizeGender()
    {
        if (dynamicCharacterAvatarScript.activeRace.name == maleRaceName)
        {
            genderMale = true;
            cameraControllerScript.genderMale = true;
        }
        else if (dynamicCharacterAvatarScript.activeRace.name == femaleRaceName)
        {
            genderMale = false;
            cameraControllerScript.genderMale = false;
        }
    }

    private void OnEnable()
    {
        dynamicCharacterAvatarScript.CharacterUpdated.AddListener(UpdateCheck);
    }

    private void OnDisable()
    {
        dynamicCharacterAvatarScript.CharacterUpdated.RemoveListener(UpdateCheck);
    }

    void UpdateCheck(UMAData data)
    {
        dNAmodule = dynamicCharacterAvatarScript.GetDNA();
    }
    #endregion

    #region DNAModule


    public void DNAValueChanged(int valueDNA)
    {
        dNAModuleValue = valueDNA;
    }

    public void DNAModuleAdjustments()
    {
        ChangeDNAModuleParameter(dNAModuleValue);
    }

    public void ChangeDNAModuleParameter(int valueDNA)
    {
        if (dNAmodule == null)
        {
            Debug.Log("no Dna module found!");
            dNAmodule = dynamicCharacterAvatarScript.GetDNA();
        }
        else
        {
            //use if using normal text
            //dNAmodule[DNAModuleStrings[valueDNA]].Set(DNAModuleSliders[valueDNA].value);

            ///use if using TMP
            dNAmodule[DNAModuleStrings[valueDNA]].Set(DNAModuleSlidersTMP[valueDNA].value);



            dynamicCharacterAvatarScript.BuildCharacter();
        }
    }

    private void DNAmoduleSliderMatch()
    {
        for (int i = 0; i < DNAModuleSlidersTMP.Count; i++)
        {
            DNAModuleSlidersTMP[i].value = DNASliderValuesTMP[i];
        }

    }

    private void DNAmodulesSliderUpdate()
    {
        for (int i = 0; i < DNAModuleSlidersTMP.Count; i++)
        {
            DNASliderValuesTMP[i] = DNAModuleSlidersTMP[i].value;
        }
    }

    #endregion


    #region Colors Module

    //Deals with color module navigation

    public void ColorItemIncrement()
    {
        if (ColorModuleValue < ColorModuleStrings.Count - 1)
        {
            ColorModuleValue++;
            colorModuleText.text = ColorModuleStrings[ColorModuleValue];
            if (ColorModuleValue == 1 || ColorModuleValue == 2)
            {
                cameraControllerScript.FocusFaceView();
            }
            else
            {
                cameraControllerScript.FocusFullbodyView();
            }
        }

        else
        {
            ColorModuleValue = 0;
            colorModuleText.text = ColorModuleStrings[ColorModuleValue];
            if (ColorModuleValue == 1 || ColorModuleValue == 2)
            {
                cameraControllerScript.FocusFaceView();
            }
            else
            {
                cameraControllerScript.FocusFullbodyView();
            }
        }
    }

    public void ColorItemDecrement()
    {
        if (ColorModuleValue > 0)
        {
            ColorModuleValue--;
            colorModuleText.text = ColorModuleStrings[ColorModuleValue];
            if (ColorModuleValue == 1 || ColorModuleValue == 2)
            {
                cameraControllerScript.FocusFaceView();
            }
            else
            {
                cameraControllerScript.FocusFullbodyView();
            }
        }
        else
        {
            ColorModuleValue = ColorModuleStrings.Count - 1;
            colorModuleText.text = ColorModuleStrings[ColorModuleValue];
            if (ColorModuleValue == 1 || ColorModuleValue == 2)
            {
                cameraControllerScript.FocusFaceView();
            }
            else
            {
                cameraControllerScript.FocusFullbodyView();
            }
        }
    }

    //Deals with updating of colors to respective items

    public void ColorModuleAdjustments(Color col)
    {
        Debug.Log("Changing Colors");

        dynamicCharacterAvatarScript.SetColor(ColorModuleStrings[ColorModuleValue], col);
        dynamicCharacterAvatarScript.UpdateColors(true);
    }
    #endregion

    #region GuiseModule

    //Deals with guise UI navigation
    private void UpdateguiseModuleMale()
    {
        guiseSubModuleText.text = "None";
        guiseSubModuleValue = 0;

        guiseModuleText.text = GuiseModuleStringsMale[guiseModuleValue];
        if (guiseModuleValue < 4)
        {
            cameraControllerScript.FocusFaceView();
        }
        else
        {
            cameraControllerScript.FocusFullbodyView();
        }
    }

    private void UpdateguiseModuleFemale()
    {
        guiseSubModuleText.text = "None";
        guiseSubModuleValue = 0;

        guiseModuleText.text = GuiseModuleStringsFemale[guiseModuleValue];
        if (guiseModuleValue < 1)
        {
            cameraControllerScript.FocusFaceView();
        }
        else
        {
            cameraControllerScript.FocusFullbodyView();
        }
    }

    public void GuiseModuleIncrement()
    {
        if (genderMale)
        {
            if (guiseModuleValue < GuiseModuleStringsMale.Count - 1)
            {
                guiseModuleValue++;
                UpdateguiseModuleMale();
            }

            else
            {
                guiseModuleValue = 0;
                UpdateguiseModuleMale();
            }
        }
        else
        {
            if (guiseModuleValue < GuiseModuleStringsFemale.Count - 1)
            {
                guiseModuleValue++;
                UpdateguiseModuleFemale();
            }

            else
            {
                guiseModuleValue = 0;
                UpdateguiseModuleFemale();
            }
        }
    }

    public void GuiseModuleDecrement()
    {
        if (genderMale)
        {
            if (guiseModuleValue > 0)
            {
                guiseModuleValue--;
                UpdateguiseModuleMale();
            }
            else
            {
                guiseModuleValue = GuiseModuleStringsMale.Count - 1;
                UpdateguiseModuleMale();
            }
        }
        else
        {
            if (guiseModuleValue > 0)
            {
                guiseModuleValue--;
                UpdateguiseModuleFemale();
            }
            else
            {
                guiseModuleValue = GuiseModuleStringsFemale.Count - 1;
                UpdateguiseModuleFemale();
            }
        }
    }

    #region Male Guise SubModules

    //updates slots with respective recipies
    public void UpdateHairModuleMale()
    {
        if (guiseSubModuleValue == 0)
        {
            dynamicCharacterAvatarScript.ClearSlot(GuiseModuleStringsMale[guiseModuleValue]);
            dynamicCharacterAvatarScript.BuildCharacter();
        }
        else
        {
            dynamicCharacterAvatarScript.SetSlot(GuiseModuleStringsMale[guiseModuleValue], hairModuleMale[guiseSubModuleValue]);
            dynamicCharacterAvatarScript.BuildCharacter();
        }
        hairModulevalueMale = guiseSubModuleValue;
        guiseSubModuleText.text = hairModuleMale[guiseSubModuleValue];

    }

    public void UpdateBeardModuleMale()
    {
        if (guiseSubModuleValue == 0)
        {
            dynamicCharacterAvatarScript.ClearSlot(GuiseModuleStringsMale[guiseModuleValue]);
            dynamicCharacterAvatarScript.BuildCharacter();
        }
        else
        {
            dynamicCharacterAvatarScript.SetSlot(GuiseModuleStringsMale[guiseModuleValue], beardModuleMale[guiseSubModuleValue]);
            dynamicCharacterAvatarScript.BuildCharacter();
        }
        guiseSubModuleText.text = beardModuleMale[guiseSubModuleValue];
        beardModulevalueMale = guiseSubModuleValue;

    }

    public void UpdateEyesModuleMale()
    {
        if (guiseSubModuleValue == 0)
        {
            dynamicCharacterAvatarScript.ClearSlot(GuiseModuleStringsMale[guiseModuleValue]);
            dynamicCharacterAvatarScript.BuildCharacter();
        }
        else
        {
            dynamicCharacterAvatarScript.SetSlot(GuiseModuleStringsMale[guiseModuleValue], eyesModuleMale[guiseSubModuleValue]);
            dynamicCharacterAvatarScript.BuildCharacter();
        }
        eyesModulevalueMale = guiseSubModuleValue;
        guiseSubModuleText.text = eyesModuleMale[guiseSubModuleValue];

    }

    public void UpdateEyeBrowsModuleMale()
    {
        if (guiseSubModuleValue == 0)
        {
            dynamicCharacterAvatarScript.ClearSlot(GuiseModuleStringsMale[guiseModuleValue]);
            dynamicCharacterAvatarScript.BuildCharacter();
        }
        else
        {
            dynamicCharacterAvatarScript.SetSlot(GuiseModuleStringsMale[guiseModuleValue], eyeBrowsModuleMale[guiseSubModuleValue]);
            dynamicCharacterAvatarScript.BuildCharacter();
        }
        eyebrowsModulevalueMale = guiseSubModuleValue;
        guiseSubModuleText.text = eyeBrowsModuleMale[guiseSubModuleValue];

    }

    public void UpdateChestModuleMale()
    {
        if (guiseSubModuleValue == 0)
        {
            dynamicCharacterAvatarScript.ClearSlot(GuiseModuleStringsMale[guiseModuleValue]);
            dynamicCharacterAvatarScript.BuildCharacter();
        }
        else
        {
            dynamicCharacterAvatarScript.SetSlot(GuiseModuleStringsMale[guiseModuleValue], chestModuleMale[guiseSubModuleValue]);
            dynamicCharacterAvatarScript.BuildCharacter();
        }
        chestModulevalueMale = guiseSubModuleValue;
        guiseSubModuleText.text = chestModuleMale[guiseSubModuleValue];

    }

    public void UpdateLegsModuleMale()
    {
        if (guiseSubModuleValue == 0)
        {
            dynamicCharacterAvatarScript.ClearSlot(GuiseModuleStringsMale[guiseModuleValue]);
            dynamicCharacterAvatarScript.BuildCharacter();
        }
        else
        {
            dynamicCharacterAvatarScript.SetSlot(GuiseModuleStringsMale[guiseModuleValue], legsModuleMale[guiseSubModuleValue]);
            dynamicCharacterAvatarScript.BuildCharacter();
        }
        LegModulevalueMale = guiseSubModuleValue;
        guiseSubModuleText.text = legsModuleMale[guiseSubModuleValue];

    }

    public void UpdateFeetModuleMale()
    {
        if (guiseSubModuleValue == 0)
        {
            dynamicCharacterAvatarScript.ClearSlot(GuiseModuleStringsMale[guiseModuleValue]);
            dynamicCharacterAvatarScript.BuildCharacter();
        }
        else
        {
            dynamicCharacterAvatarScript.SetSlot(GuiseModuleStringsMale[guiseModuleValue], feetModuleMale[guiseSubModuleValue]);
            dynamicCharacterAvatarScript.BuildCharacter();
        }
        feetModulevalueMale = guiseSubModuleValue;
        guiseSubModuleText.text = feetModuleMale[guiseSubModuleValue];

    }

    //replicate the funtion if you need to add your own slot here
    #endregion

    #region Female Guise SubModules

    // deals with female UMA race as you add race you need to replicate this modules accordingly.
    public void UpdateHairModuleFemale()
    {
        if (guiseSubModuleValue == 0)
        {
            dynamicCharacterAvatarScript.ClearSlot(GuiseModuleStringsFemale[guiseModuleValue]);
            dynamicCharacterAvatarScript.BuildCharacter();
        }
        else
        {
            dynamicCharacterAvatarScript.SetSlot(GuiseModuleStringsFemale[guiseModuleValue], hairModuleFemale[guiseSubModuleValue]);
            dynamicCharacterAvatarScript.BuildCharacter();
        }
        guiseSubModuleText.text = hairModuleFemale[guiseSubModuleValue];

    }

    public void UpdateChestModuleFemale()
    {
        if (guiseSubModuleValue == 0)
        {
            dynamicCharacterAvatarScript.ClearSlot(GuiseModuleStringsFemale[guiseModuleValue]);
            dynamicCharacterAvatarScript.BuildCharacter();
        }
        else
        {
            dynamicCharacterAvatarScript.SetSlot(GuiseModuleStringsFemale[guiseModuleValue], chestModuleFemale[guiseSubModuleValue]);
            dynamicCharacterAvatarScript.BuildCharacter();
        }
        guiseSubModuleText.text = chestModuleFemale[guiseSubModuleValue];

    }

    public void UpdateLegsModuleFemale()
    {
        if (guiseSubModuleValue == 0)
        {
            dynamicCharacterAvatarScript.ClearSlot(GuiseModuleStringsFemale[guiseModuleValue]);
            dynamicCharacterAvatarScript.BuildCharacter();
        }
        else
        {
            dynamicCharacterAvatarScript.SetSlot(GuiseModuleStringsFemale[guiseModuleValue], legsModuleFemale[guiseSubModuleValue]);
            dynamicCharacterAvatarScript.BuildCharacter();
        }
        guiseSubModuleText.text = legsModuleFemale[guiseSubModuleValue];

    }

    public void UpdateFeetModuleFemale()
    {
        if (guiseSubModuleValue == 0)
        {
            dynamicCharacterAvatarScript.ClearSlot(GuiseModuleStringsFemale[guiseModuleValue]);
            dynamicCharacterAvatarScript.BuildCharacter();
        }
        else
        {
            dynamicCharacterAvatarScript.SetSlot(GuiseModuleStringsFemale[guiseModuleValue], feetModuleFemale[guiseSubModuleValue]);
            dynamicCharacterAvatarScript.BuildCharacter();
        }
        guiseSubModuleText.text = feetModuleFemale[guiseSubModuleValue];

    }

    public void UpdateUnderwearModuleFemale()
    {
        if (guiseSubModuleValue == 0)
        {

        }
        else
        {
            dynamicCharacterAvatarScript.SetSlot(GuiseModuleStringsFemale[guiseModuleValue], UnderwearModuleFemale[guiseSubModuleValue]);
            dynamicCharacterAvatarScript.BuildCharacter();
        }
        guiseSubModuleText.text = UnderwearModuleFemale[guiseSubModuleValue];

    }
    #endregion
    public void GuiseSubModuleIncrement()
    {
        //deals with selection of recipes and implements the recipe to the character creator
        if (genderMale)
        {
            switch (guiseModuleValue)
            {

                case 0:
                    if (guiseSubModuleValue < hairModuleMale.Count - 1)
                    {
                        guiseSubModuleValue++;
                        UpdateHairModuleMale();

                    }

                    else
                    {
                        guiseSubModuleValue = 0;
                        UpdateHairModuleMale();
                    }
                    break;

                case 1:
                    if (guiseSubModuleValue < beardModuleMale.Count - 1)
                    {
                        guiseSubModuleValue++;
                        UpdateBeardModuleMale();

                    }

                    else
                    {
                        guiseSubModuleValue = 0;
                        UpdateBeardModuleMale();
                    }
                    break;

                case 2:
                    if (guiseSubModuleValue < eyesModuleMale.Count - 1)
                    {
                        guiseSubModuleValue++;
                        UpdateEyesModuleMale();

                    }

                    else
                    {
                        guiseSubModuleValue = 0;
                        UpdateEyesModuleMale();
                    }

                    break;

                case 3:
                    if (guiseSubModuleValue < eyeBrowsModuleMale.Count - 1)
                    {
                        guiseSubModuleValue++;
                        UpdateEyeBrowsModuleMale();

                    }

                    else
                    {
                        guiseSubModuleValue = 0;
                        UpdateEyeBrowsModuleMale();
                    }

                    break;

                case 4:
                    if (guiseSubModuleValue < chestModuleMale.Count - 1)
                    {
                        guiseSubModuleValue++;
                        UpdateChestModuleMale();

                    }

                    else
                    {
                        guiseSubModuleValue = 0;
                        UpdateChestModuleMale();
                    }

                    break;

                case 5:
                    if (guiseSubModuleValue < legsModuleMale.Count - 1)
                    {
                        guiseSubModuleValue++;
                        UpdateLegsModuleMale();

                    }

                    else
                    {
                        guiseSubModuleValue = 0;
                        UpdateLegsModuleMale();
                    }

                    break;

                case 6:
                    if (guiseSubModuleValue < feetModuleMale.Count - 1)
                    {
                        guiseSubModuleValue++;
                        UpdateFeetModuleMale();

                    }

                    else
                    {
                        guiseSubModuleValue = 0;
                        UpdateFeetModuleMale();
                    }

                    break;

                    //Add a new case after adding the slot name to the guisemodulelist to get the slot functional as earlier recipes replicate this step wherver necessary
            }
        }
        else
        {
            switch (guiseModuleValue)
            {

                case 0:
                    if (guiseSubModuleValue < hairModuleFemale.Count - 1)
                    {
                        guiseSubModuleValue++;
                        UpdateHairModuleFemale();

                    }

                    else
                    {
                        guiseSubModuleValue = 0;
                        UpdateHairModuleFemale();
                    }
                    break;

                case 1:
                    if (guiseSubModuleValue < chestModuleFemale.Count - 1)
                    {
                        guiseSubModuleValue++;
                        UpdateChestModuleFemale();

                    }

                    else
                    {
                        guiseSubModuleValue = 0;
                        UpdateChestModuleFemale();
                    }
                    break;

                case 2:
                    if (guiseSubModuleValue < legsModuleFemale.Count - 1)
                    {
                        guiseSubModuleValue++;
                        UpdateLegsModuleFemale();

                    }

                    else
                    {
                        guiseSubModuleValue = 0;
                        UpdateLegsModuleFemale();
                    }
                    break;

                case 3:
                    if (guiseSubModuleValue < feetModuleFemale.Count - 1)
                    {
                        guiseSubModuleValue++;
                        UpdateFeetModuleFemale();

                    }

                    else
                    {
                        guiseSubModuleValue = 0;
                        UpdateFeetModuleFemale();
                    }

                    break;

                case 4:
                    if (guiseSubModuleValue < UnderwearModuleFemale.Count - 1)
                    {
                        guiseSubModuleValue++;
                        UpdateUnderwearModuleFemale();

                    }

                    else
                    {
                        guiseSubModuleValue = 0;
                        UpdateUnderwearModuleFemale();
                    }

                    break;
            }
        }
    }

    public void GuiseSubModuleDecrement()
    {
        if (genderMale)
        {
            switch (guiseModuleValue)
            {
                case 0:
                    if (guiseSubModuleValue > 0)
                    {
                        guiseSubModuleValue--;
                        UpdateHairModuleMale();

                    }

                    else
                    {
                        guiseSubModuleValue = hairModuleMale.Count - 1;
                        UpdateHairModuleMale();
                    }
                    break;

                case 1:
                    if (guiseSubModuleValue > 0)
                    {
                        guiseSubModuleValue--;
                        UpdateBeardModuleMale();

                    }

                    else
                    {
                        guiseSubModuleValue = beardModuleMale.Count - 1;
                        UpdateBeardModuleMale();
                    }
                    break;

                case 2:
                    if (guiseSubModuleValue > 0)
                    {
                        guiseSubModuleValue--;
                        UpdateEyesModuleMale();

                    }

                    else
                    {
                        guiseSubModuleValue = eyesModuleMale.Count - 1;
                        UpdateEyesModuleMale();
                    }
                    break;

                case 3:
                    if (guiseSubModuleValue > 0)
                    {
                        guiseSubModuleValue--;
                        UpdateEyeBrowsModuleMale();

                    }

                    else
                    {
                        guiseSubModuleValue = eyeBrowsModuleMale.Count - 1;
                        UpdateEyeBrowsModuleMale();
                    }
                    break;

                case 4:
                    if (guiseSubModuleValue > 0)
                    {
                        guiseSubModuleValue--;
                        UpdateChestModuleMale();

                    }

                    else
                    {
                        guiseSubModuleValue = chestModuleMale.Count - 1;
                        UpdateChestModuleMale();
                    }
                    break;

                case 5:
                    if (guiseSubModuleValue > 0)
                    {
                        guiseSubModuleValue--;
                        UpdateLegsModuleMale();

                    }

                    else
                    {
                        guiseSubModuleValue = legsModuleMale.Count - 1;
                        UpdateLegsModuleMale();
                    }
                    break;

                case 6:
                    if (guiseSubModuleValue > 0)
                    {
                        guiseSubModuleValue--;
                        UpdateFeetModuleMale();

                    }

                    else
                    {
                        guiseSubModuleValue = feetModuleMale.Count - 1;
                        UpdateFeetModuleMale();
                    }
                    break;
            }
        }
        else
        {
            switch (guiseModuleValue)
            {
                case 0:
                    if (guiseSubModuleValue > 0)
                    {
                        guiseSubModuleValue--;
                        UpdateHairModuleFemale();

                    }

                    else
                    {
                        guiseSubModuleValue = hairModuleFemale.Count - 1;
                        UpdateHairModuleFemale();
                    }
                    break;

                case 1:
                    if (guiseSubModuleValue > 0)
                    {
                        guiseSubModuleValue--;
                        UpdateChestModuleFemale();

                    }

                    else
                    {
                        guiseSubModuleValue = chestModuleFemale.Count - 1;
                        UpdateChestModuleFemale();
                    }
                    break;

                case 2:
                    if (guiseSubModuleValue > 0)
                    {
                        guiseSubModuleValue--;
                        UpdateLegsModuleFemale();

                    }

                    else
                    {
                        guiseSubModuleValue = legsModuleFemale.Count - 1;
                        UpdateLegsModuleFemale();
                    }
                    break;

                case 3:
                    if (guiseSubModuleValue > 0)
                    {
                        guiseSubModuleValue--;
                        UpdateFeetModuleFemale();

                    }

                    else
                    {
                        guiseSubModuleValue = feetModuleFemale.Count - 1;
                        UpdateFeetModuleFemale();
                    }
                    break;

                case 4:
                    if (guiseSubModuleValue > 0)
                    {
                        guiseSubModuleValue--;
                        UpdateUnderwearModuleFemale();

                    }

                    else
                    {
                        guiseSubModuleValue = UnderwearModuleFemale.Count - 1;
                        UpdateUnderwearModuleFemale();
                    }
                    break;


            }
        }


    }

    public void GuiseSubModuleUpdate()
    {
        //deals with selection of recipes and implements the recipe to the character creator
        if (genderMale)
        {
            switch (guiseModuleValue)
            {

                case 0:
                    guiseSubModuleValue = hairModulevalueMale;
                    UpdateHairModuleMale();

                    break;

                case 1:
                    guiseSubModuleValue = beardModulevalueMale;
                    UpdateBeardModuleMale();

                    break;

                case 2:
                    guiseSubModuleValue = eyesModulevalueMale;
                    UpdateEyesModuleMale();

                    break;

                case 3:
                    guiseSubModuleValue = eyebrowsModulevalueMale;
                    UpdateEyeBrowsModuleMale();

                    break;

                case 4:
                    guiseSubModuleValue = chestModulevalueMale;
                    UpdateChestModuleMale();
                    break;

                case 5:
                    guiseSubModuleValue = LegModulevalueMale;
                    UpdateLegsModuleMale();

                    break;

                case 6:
                    guiseSubModuleValue = feetModulevalueMale;
                    UpdateFeetModuleMale();

                    break;

                    //Add a new case after adding the slot name to the guisemodulelist to get the slot functional as earlier recipes replicate this step wherver necessary
            }
        }
        else
        {
            switch (guiseModuleValue)
            {

                case 0:
                    guiseSubModuleValue = hairModulevalueFemale;
                    UpdateHairModuleFemale();

                    break;

                case 1:
                    guiseSubModuleValue = chestModulevalueFemale;
                    UpdateChestModuleFemale();

                    break;

                case 2:
                    guiseSubModuleValue = LegModulevalueFemale;
                    UpdateLegsModuleFemale();

                    break;

                case 3:
                    guiseSubModuleValue = feetModulevalueFemale;
                    UpdateFeetModuleFemale();

                    break;

                case 4:
                    guiseSubModuleValue = underwearModulevalueFemale;
                    UpdateUnderwearModuleFemale();

                    break;
            }
        }
    }

    #endregion


    #region Save/Load 

    public void SaveCharacter()
    {
        CharacterData = dynamicCharacterAvatarScript.GetCurrentRecipe();
        DNAmodulesSliderUpdate();
        Debug.Log(CharacterData);

        // you have all the slider values here below

        //DNASliderValuesTMP[];
    }

    public void LoadCharacter()
    {
        dynamicCharacterAvatarScript.LoadFromRecipeString(CharacterData);
        RecognizeGender();
        DNAmoduleSliderMatch();
    }

    public void GoBack()
    {
        FindObjectOfType<MainMenuManager>().GetSceneBack();
    }

    #endregion
}
