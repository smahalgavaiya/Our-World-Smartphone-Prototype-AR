using System.Collections;
using UnityEngine ;
using UnityEngine.UI ;

public class LevelProgressUI : MonoBehaviour {

   [Header ("UI references :")]
   [SerializeField] private Image uiFillImage ;
   [SerializeField] private Text uiStartText ;
   [SerializeField] private Text uiEndText ;

    float currentKarmaValue = .5f;


   private void Start () {
      
   }


   public void SetLevelTexts (int currentKarmaPoints) {
      uiStartText.text = "Lvl "+Mathf.FloorToInt(PlayerPrefs.GetInt("KarmaPoints") / 100f).ToString () ;
        int pointsNeedeForLevelUp = 100-(currentKarmaPoints % 100);
      uiEndText.text = pointsNeedeForLevelUp+" More";
   }

    private IEnumerator fillAnim(float duration)
    {
        float time = 0.0f;
        while (time < duration)
        {
            uiFillImage.fillAmount = time / duration;
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        uiFillImage.fillAmount = currentKarmaValue;
    }

   public void UpdateProgressFill (float value) {
      currentKarmaValue = value;
      uiFillImage.fillAmount = value ;
        //play animation only when we level up
      string karmaPrefString = "karmaLevelAnim";
        if (!PlayerPrefs.HasKey(karmaPrefString))
        {
            StartCoroutine(fillAnim(2.5f));
            PlayerPrefs.SetString(karmaPrefString, uiStartText.text);
        }
        else 
        {
            if (PlayerPrefs.GetString(karmaPrefString) != uiStartText.text)
                StartCoroutine(fillAnim(2.5f));

            PlayerPrefs.SetString(karmaPrefString, uiStartText.text);
        }
      
    }
}
