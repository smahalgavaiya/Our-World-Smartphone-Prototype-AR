using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private IEnumerator _autoSwitchTitleScreen;

    public GameObject _mainMenu;
    public GameObject _settingsMenu;
    public GameObject _optionsMenu;
    public GameObject _gameplayMenu;
    public GameObject _videoMenu;
    public GameObject _audioMenu;
    public MainMenuIntro _mainMenuIntro;
    public GameObject _sceneHolder;

    [Header("Gameplay Panel Settings")]
    public Toggle _autoSwitchToggle;
    public Slider _autoSwitchSlider;
    public TextMeshProUGUI _autoSwitchSliderValue;

    private string _activeOption;

    private void Start()
    {
        ConfigureMenu();
    }

    private void ConfigureMenu()
    {
        _autoSwitchToggle.isOn = PlayerPrefs.GetInt("AutoSwitchToggle", 0) == 0 ? false : true;
        AutoSwitchSlider(PlayerPrefs.GetFloat("AutoSwitchSlider", 10));
    }

    private void OnEnable()
    {
        LeanTween.value(-90, 30, 2).setEaseOutSine().setOnUpdate((float value) =>
        {
            _mainMenu.transform.localEulerAngles = new Vector3(0, value, 0);
        });
    }

    public void MenuButtonPressed(string name)
    {
        if (name == "Continue")
            return;
        else if (name == "NewGame")
            NewGame();
        else if (name == "UMACC")
        {
            _sceneHolder.SetActive(false);
            SceneManager.LoadScene("UMACC", LoadSceneMode.Additive);
        }
        else if (name == "Settings")
        {
            MainMenuTransition(_mainMenu, _settingsMenu);
            _audioMenu.SetActive(false);
            _videoMenu.SetActive(false);
            _optionsMenu.SetActive(true);
            OptionsMenuTransition("GamePlay");
        }
        else if (name == "Quit")
            Quit();
    }

    public void GetSceneBack()
    {
        _sceneHolder.SetActive(true);
        SceneManager.UnloadSceneAsync("UMACC");
    }

    public void SettingsButtonPressed(string name)
    {
        if (name == "Back")
        {
            MainMenuTransition(_settingsMenu, _mainMenu);
            if (_activeOption == "GamePlay")
            {
                _audioMenu.SetActive(false);
                _videoMenu.SetActive(false);
                LeanTween.rotateLocal(_optionsMenu, new Vector3(0, 240, 0), 2).setEaseInOutSine().setOnComplete(() =>
                {
                    _optionsMenu.SetActive(false);
                    _audioMenu.SetActive(true);
                    _videoMenu.SetActive(true);
                    _optionsMenu.transform.localEulerAngles = new Vector3(0, 120, 0);
                });
            }
            else if (_activeOption == "Video")
            {
                _audioMenu.SetActive(false);
                _gameplayMenu.SetActive(false);
                LeanTween.rotateLocal(_optionsMenu, new Vector3(0, 0, 0), 2).setEaseInOutSine().setOnComplete(() =>
                {
                    _optionsMenu.SetActive(false);
                    _audioMenu.SetActive(true);
                    _gameplayMenu.SetActive(true);
                    _optionsMenu.transform.localEulerAngles = new Vector3(0, 120, 0);
                });
            }
            else if (_activeOption == "Audio")
            {
                _videoMenu.SetActive(false);
                _gameplayMenu.SetActive(false);
                LeanTween.rotateLocal(_optionsMenu, new Vector3(0, 120, 0), 2).setEaseInOutSine().setOnComplete(() =>
                {
                    _optionsMenu.SetActive(false);
                    _videoMenu.SetActive(true);
                    _gameplayMenu.SetActive(true);
                    _optionsMenu.transform.localEulerAngles = new Vector3(0, 120, 0);
                });
            }
        }
        else
            OptionsMenuTransition(name);
    }

    private void MainMenuTransition(GameObject exit, GameObject enter)
    {
        LeanTween.value(30, 180, 2).setEaseOutSine().setOnUpdate((float value) =>
        {
            exit.transform.localEulerAngles = new Vector3(0, value, 0);
        });
        LeanTween.value(-90, 30, 2).setEaseOutSine().setOnUpdate((float value) =>
        {
            enter.transform.localEulerAngles = new Vector3(0, value, 0);
        });
    }

    private void OptionsMenuTransition(string name)
    {
        _activeOption = name;
        if (name == "GamePlay")
            LeanTween.rotateLocal(_optionsMenu, new Vector3(0, 0, 0), 2).setEaseInOutSine().setOnComplete(() =>
            {
                _audioMenu.SetActive(true);
                _videoMenu.SetActive(true);
            });
        else if (name == "Video")
            LeanTween.rotateLocal(_optionsMenu, new Vector3(0, 120, 0), 2).setEaseInOutSine();
        else if (name == "Audio")
            LeanTween.rotateLocal(_optionsMenu, new Vector3(0, 240, 0), 2).setEaseInOutSine();
    }

    public void PreferredTitleScreen(int screenNumber)
    {
        if (PlayerPrefs.GetInt("IntroDesign", 0) != screenNumber)
        {
            if (PlayerPrefs.GetInt("AutoSwitchToggle", 0) == 0 ? false : true == true)
                SetNewCoroutine(ref _autoSwitchTitleScreen, ChangeTitleScreen());
            else if (_autoSwitchTitleScreen != null)
                StopCoroutine(_autoSwitchTitleScreen);
            PlayerPrefs.SetInt("IntroDesign", screenNumber);
            if (screenNumber == 1)
                _mainMenuIntro.Design2To1();
            else if (screenNumber == 2)
                _mainMenuIntro.Design1To2();
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene("UnityWorldSpace");
    }

    public void AutoSwitchToggle(bool value)
    {
        if (value)
            SetNewCoroutine(ref _autoSwitchTitleScreen, ChangeTitleScreen());
        if (!value)
            if (_autoSwitchTitleScreen != null)
                StopCoroutine(_autoSwitchTitleScreen);

        PlayerPrefs.SetInt("AutoSwitchToggle", value == true ? 1 : 0);
        _autoSwitchSlider.interactable = value;
    }

    public void AutoSwitchSlider(float value)
    {
        PlayerPrefs.SetFloat("AutoSwitchSlider", value);
        _autoSwitchSliderValue.text = value.ToString();
        if (PlayerPrefs.GetInt("AutoSwitchToggle", 0) == 0 ? false : true == true)
            SetNewCoroutine(ref _autoSwitchTitleScreen, ChangeTitleScreen());
        else if (_autoSwitchTitleScreen != null)
            StopCoroutine(_autoSwitchTitleScreen);
    }

    private void SetNewCoroutine(ref IEnumerator _coroutineValue, IEnumerator _coroutineName)
    {
        if (_coroutineValue != null)
            StopCoroutine(_coroutineValue);
        _coroutineValue = _coroutineName;
        StartCoroutine(_coroutineValue);
    }

    private IEnumerator ChangeTitleScreen()
    {
        yield return new WaitForSeconds(PlayerPrefs.GetFloat("AutoSwitchSlider", 10));
        PreferredTitleScreen(PlayerPrefs.GetInt("IntroDesign", 1) == 1 ? 2 : 1);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}