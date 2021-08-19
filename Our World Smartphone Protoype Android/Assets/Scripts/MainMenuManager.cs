using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour 
{
	public GameObject _mainMenu;
	public GameObject _settingsMenu;
	public GameObject _optionsMenu;
	public GameObject _gameplayMenu;
	public GameObject _videoMenu;
	public GameObject _audioMenu;
	public MainMenuIntro _mainMenuIntro;

	private string _activeOption;

	private void OnEnable()
	{
		LeanTween.value(-90, 30, 2).setEaseOutSine().setOnUpdate((float value) => {
			_mainMenu.transform.localEulerAngles = new Vector3(0, value, 0);
		});
	}

	public void MenuButtonPressed(string name)
	{
		if (name == "Continue")
			return;
		else if (name == "NewGame")
			NewGame();
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

	public void SettingsButtonPressed(string name)
	{
		if (name == "Back")
		{
			MainMenuTransition(_settingsMenu, _mainMenu);
			if (_activeOption == "GamePlay")
			{
				_audioMenu.SetActive(false);
				_videoMenu.SetActive(false);
				LeanTween.rotateLocal(_optionsMenu, new Vector3(0, 240, 0), 2).setEaseInOutSine().setOnComplete(() => {
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
				LeanTween.rotateLocal(_optionsMenu, new Vector3(0, 0, 0), 2).setEaseInOutSine().setOnComplete(() => {
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
				LeanTween.rotateLocal(_optionsMenu, new Vector3(0, 120, 0), 2).setEaseInOutSine().setOnComplete(() => {
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
		LeanTween.value(30, 180, 2).setEaseOutSine().setOnUpdate((float value) => {
			exit.transform.localEulerAngles = new Vector3(0, value, 0);
		});
		LeanTween.value(-90, 30, 2).setEaseOutSine().setOnUpdate((float value) => {
			enter.transform.localEulerAngles = new Vector3(0, value, 0);
		});
	}

	private void OptionsMenuTransition(string name)
	{
		_activeOption = name;
		if (name == "GamePlay")
			LeanTween.rotateLocal(_optionsMenu, new Vector3(0, 0, 0), 2).setEaseInOutSine().setOnComplete(() => {
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

    public void Quit () 
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}