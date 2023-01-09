using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuAvatarInfoManager : MonoBehaviour
{
    public TextMeshProUGUI _avatarName;
    public TextMeshProUGUI _avatarLevel;

    public TMP_Text karmaValueLabelText, HealthValueLabelText, ManaValueLabelText;

    public Slider HPSlider,  ManaSlider;
    public LevelProgressUI karmaSLiderLevelProgressBar;
    private void Start()
    {
        UpdateDetails();
        AvatarInfoManager.Instance.mainMenuAvatarInfoManager = this;
    }

    public void UpdateDetails()
    {
        _avatarName.text = PlayerPrefs.GetString("AvatarName");
        _avatarLevel.text = PlayerPrefs.GetString("AvatarLevel");
        HPSlider.value = PlayerPrefs.GetInt("HealthPoints") /100f;
        karmaSLiderLevelProgressBar.SetLevelTexts(PlayerPrefs.GetInt("KarmaPoints"));
        karmaSLiderLevelProgressBar.UpdateProgressFill((PlayerPrefs.GetInt("KarmaPoints")%100) / 100f);
        ManaSlider.value = PlayerPrefs.GetInt("ManaPoints") / 100f;



        karmaValueLabelText.text = 
            "Karma: " + PlayerPrefs.GetInt("KarmaPoints") + "/"+ 
            ((100 - (PlayerPrefs.GetInt("KarmaPoints") % 100))+ PlayerPrefs.GetInt("KarmaPoints"));
        HealthValueLabelText.text = "Health: " +PlayerPrefs.GetInt("HealthPoints")  + "/100";
        ManaValueLabelText.text = "Mana: " + PlayerPrefs.GetInt("ManaPoints") + "/100";
    }
}
