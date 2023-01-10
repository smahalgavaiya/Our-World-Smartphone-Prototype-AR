using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuAvatarInfoManager : MonoBehaviour
{
    public TextMeshProUGUI _avatarName;
    public TextMeshProUGUI _avatarLevel;

    public Slider HPSlider, KarmaSlider, ManaSlider;
    private void Start()
    {
        UpdateDetails();
    }

    private void UpdateDetails()
    {
        _avatarName.text = PlayerPrefs.GetString("AvatarName");
        _avatarLevel.text = PlayerPrefs.GetString("AvatarLevel");
        HPSlider.value = PlayerPrefs.GetInt("HealthPoints") /100f;
        KarmaSlider.value = PlayerPrefs.GetInt("KarmaPoints") / 100f;
        ManaSlider.value = PlayerPrefs.GetInt("ManaPoints") / 100f;
    }
}
