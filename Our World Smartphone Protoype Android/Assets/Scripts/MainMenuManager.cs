using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuManager : MonoBehaviour {


    public Button ButtonNewGame;

    // Use this for initialization
    void Start()
    {
     //   Button btnNewGame = ButtonNewGame.GetComponent<Button>();
       // btnNewGame.onClick.AddListener(NewGame);
    }

    public void NewGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("UnityWorldSpace");
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
