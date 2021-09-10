using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class MainMenuIntro : MonoBehaviour
{
    public GameObject _earth;
    public GameObject _earthModel;
    public Camera _cam;
    public GameObject _SunLensFlareHolder;
    public SpriteRenderer _SunLensFlare1;
    public SpriteRenderer _SunLensFlare2;
    public SpriteRenderer _SunLensFlare3;
    public GameObject _gameTitle;
    public TextMeshProUGUI _gameTitleText;
    public GameObject _gameMotto;
    public GameObject _sceneHolder;
    public GameObject _stars;
    public GameObject _starFlare;
    public Volume _introVolume;
    public Volume _finalVolume;
    public Sprite _newLensFlare;
    public Sprite _introLensFlare;
    public GameObject _canvas;
    public Transform _ourWorldNameCanvas;
    public TextMeshPro _ourWorldNameCanvasText;
    public GameObject _3DCanvas;
    public Material _skyboxMat;
    public Material _earthMat;

    private ObjectRotation[] OR;

    private void Start()
    {
        OR = FindObjectsOfType<ObjectRotation>();
        DoIntro();
    }

    public void DoIntro()
    {
        if (PlayerPrefs.GetInt("IntroDesign", 1) == 1)
            Intro1();
        else if (PlayerPrefs.GetInt("IntroDesign", 1) == 2)
            Intro2();
    }

    #region Design 1
    private void Intro1()
    {
        _skyboxMat.SetFloat("_Exposure", 0);
        _earthMat.SetVector("_EarthBrightness", new Vector4(0, 0, 0, 0));
        LeanTween.moveLocalY(_earth, -0.53f, 4).setEaseOutSine().setOnComplete(() =>
        {
            Invoke("SunRise", 3);
        });
    }

    private void SunRise()
    {
        LeanTween.value(29f, 22f, 6).setEaseOutSine().setOnUpdate((float value) =>
        {
            _cam.fieldOfView = value;
        });
        LeanTween.value(0, 1, 6).setEaseOutSine().setOnUpdate((float value) =>
        {
            _skyboxMat.SetFloat("_Exposure", value);
            _earthMat.SetVector("_EarthBrightness", new Vector4(value, value, value, value));
        });
        LeanTween.value(0, 1, 3).setOnComplete(() => {
            LeanTween.moveLocalZ(_gameTitle, 0, 2).setEaseOutSine();
        });
        LeanTween.value(0, 1, 4).setOnComplete(() => {
            LeanTween.moveLocalZ(_gameMotto, 0, 2).setEaseOutSine().setOnComplete(() =>
            {
                Invoke("Transition", 3);
            });
        });
        LeanTween.value(0, 1, 6).setEaseOutSine().setOnUpdate((float value) =>
        {
            _SunLensFlare1.color = new Color(1, 1, 1, value);
            _SunLensFlare3.color = new Color(1, 1, 1, value);
        });
        LeanTween.moveLocalY(_SunLensFlareHolder, 3.9f, 6).setEaseOutSine().setOnComplete(() =>
        {
            _SunLensFlare1.GetComponent<Animator>().enabled = true;
            _SunLensFlare2.GetComponent<Animator>().enabled = true;
        });
    }

    private void Transition()
    {
        LeanTween.moveLocalZ(_gameMotto, -1500, 2).setEaseInSine().setOnComplete(() =>
        {
            _gameMotto.SetActive(false);
            ObjectRotationState(false);
            SwapLensFlareSprite(_newLensFlare, false);
            LeanTween.rotateLocal(_sceneHolder, new Vector3(26.7f, 173.5f, -4.36f), 2).setEaseOutSine();
            LeanTween.moveLocal(_earth, new Vector3(-0.65f, -0.4f, -1.57f), 3).setEaseOutSine();
            LeanTween.rotateLocal(_earthModel, new Vector3(0f, 0f, 0f), 3).setEaseOutSine();
            LeanTween.moveLocalZ(_stars, -45, 3).setEaseOutSine();
            LeanTween.moveLocalZ(_starFlare, 0, 3).setEaseOutSine();
            LeanTween.moveLocal(_SunLensFlareHolder, new Vector3(-1.85f, -0.36f, -16f), 3).setEaseOutSine();
            LeanTween.scale(_SunLensFlare1.gameObject, new Vector3(1, 1, 1), 3f).setEaseOutSine();
            LeanTween.scale(_SunLensFlare2.gameObject, new Vector3(1, 1, 1), 3f).setEaseOutSine();
            LeanTween.scale(_SunLensFlare3.gameObject, new Vector3(1, 1, 1), 3f).setEaseOutSine();
            LeanTween.value(22f, 35f, 3).setEaseOutSine().setOnUpdate((float value) =>
            {
                _cam.fieldOfView = value;
            });
            LeanTween.value(1, 0, 3).setEaseOutSine().setOnUpdate((float value) =>
            {
                _introVolume.weight = value;
                _finalVolume.weight = 1 - value;
            }).setOnComplete(() =>
            {
                ChangeEarthRotationAxis(ObjectRotation.AxisToRotateIn.UP);
                ObjectRotationState(true);
                OurWorldLogoCanvas();
            });
        });
    }

    private void OurWorldLogoCanvas()
    {
        _canvas.SetActive(true);
        _gameTitle.transform.SetParent(_ourWorldNameCanvas);
        LeanTween.moveLocal(_gameTitle, new Vector3(0, 0, 0), 2).setEaseOutSine();
        LeanTween.scale(_gameTitle, new Vector3(1, 1, 1), 2).setEaseOutSine();
        LeanTween.rotateLocal(_gameTitle, new Vector3(0, 0, 0), 2).setEaseOutSine();
        LeanTween.value(190, 90, 2).setEaseOutSine().setOnUpdate((float value) =>
        {
            _gameTitleText.fontSize = value;
        }).setOnComplete(() =>
        {
            _3DCanvas.SetActive(false);
            _ourWorldNameCanvasText.GetComponent<TextMeshPro>().text = "OUR WORLD";
        });
    }

    private void SwapLensFlareSprite(Sprite lensFlare, bool lensFlare3)
    {
        _SunLensFlare1.GetComponent<Animator>().enabled = false;
        _SunLensFlare2.GetComponent<Animator>().enabled = false;
        LeanTween.value(0.85f, 0, 1.5f).setEaseOutSine().setOnUpdate(UpdateLensFlareOpacityFinal).setOnComplete(() =>
        {
            _SunLensFlare1.sprite = lensFlare;
            _SunLensFlare2.sprite = lensFlare;
            _SunLensFlare3.sprite = lensFlare;
            _SunLensFlare3.gameObject.SetActive(lensFlare3);
            LeanTween.value(0, 0.85f, 1.5f).setEaseOutSine().setOnUpdate(UpdateLensFlareOpacityFinal).setOnComplete(() =>
            {
                _SunLensFlare1.GetComponent<Animator>().enabled = true;
                _SunLensFlare2.GetComponent<Animator>().enabled = true;
            });
        });
    }

    private void UpdateLensFlareOpacityFinal(float value)
    {
        _SunLensFlare1.color = new Color(1, 1, 1, value);
        if (value <= 0.15f)
            _SunLensFlare2.color = new Color(1, 1, 1, value);
        _SunLensFlare3.color = new Color(1, 1, 1, value);
    }

    private void ChangeEarthRotationAxis(ObjectRotation.AxisToRotateIn axis)
    {
        for (int i = 0; i < OR.Length; i++)
        {
            OR[i].Axis = axis;
            OR[i].SetAxis();
        }
    }

    private void ObjectRotationState(bool state)
    {
        for (int i = 0; i < OR.Length; i++)
        {
            if (state)
                OR[i].StartRotation();
            else
                OR[i].StopRotation();
        }
    }
    #endregion

    #region Design 2
    private void Intro2()
    {
        _skyboxMat.SetFloat("_Exposure", 0);
        _earthMat.SetVector("_EarthBrightness", new Vector4(0, 0, 0, 0));
        LeanTween.moveLocalY(_earth, -0.53f, 4).setEaseOutSine().setOnComplete(() =>
        {
            Invoke("SunRise2", 3);
        });
    }

    private void SunRise2()
    {
        LeanTween.value(29f, 22f, 6).setEaseOutSine().setOnUpdate((float value) =>
        {
            _cam.fieldOfView = value;
        });
        LeanTween.value(0, 1, 6).setEaseOutSine().setOnUpdate((float value) =>
        {
            _skyboxMat.SetFloat("_Exposure", value);
            _earthMat.SetVector("_EarthBrightness", new Vector4(value, value, value, value));
        });
        LeanTween.value(0, 1, 3).setOnComplete(() => {
            LeanTween.moveLocalZ(_gameTitle, 0, 2).setEaseOutSine();
        });
        LeanTween.value(0, 1, 4).setOnComplete(() => {
            LeanTween.moveLocalZ(_gameMotto, 0, 2).setEaseOutSine().setOnComplete(() =>
            {
                Invoke("Transition", 3);
            });
        });
        LeanTween.value(0, 1, 6).setEaseOutSine().setOnUpdate((float value) =>
        {
            _SunLensFlare1.color = new Color(1, 1, 1, value);
            _SunLensFlare3.color = new Color(1, 1, 1, value);
        });
        LeanTween.moveLocalY(_SunLensFlareHolder, 3.9f, 6).setEaseOutSine().setOnComplete(() =>
        {
            _SunLensFlare1.GetComponent<Animator>().enabled = true;
            _SunLensFlare2.GetComponent<Animator>().enabled = true;
        });
    }

    private void Transition2()
    {
        LeanTween.moveLocalZ(_gameMotto, -1500, 2).setEaseInSine().setOnComplete(() =>
        {
            _gameMotto.SetActive(false);
            OurWorldLogoCanvas2();
        });
    }

    private void OurWorldLogoCanvas2()
    {
        _canvas.SetActive(true);
        _gameTitle.transform.SetParent(_ourWorldNameCanvas);
        LeanTween.scale(_SunLensFlare1.gameObject, new Vector3(1.5f, 1.5f, 1.5f), 2f).setEaseOutSine();
        LeanTween.scale(_SunLensFlare2.gameObject, new Vector3(1.5f, 1.5f, 1.5f), 2f).setEaseOutSine();
        LeanTween.scale(_SunLensFlare3.gameObject, new Vector3(1.5f, 1.5f, 1.5f), 2f).setEaseOutSine();
        LeanTween.moveLocal(_gameTitle, new Vector3(0, 0, 0), 2).setEaseOutSine();
        LeanTween.scale(_gameTitle, new Vector3(1, 1, 1), 2).setEaseOutSine();
        LeanTween.rotateLocal(_gameTitle, new Vector3(0, 0, 0), 2).setEaseOutSine();
        LeanTween.value(190, 90, 2).setEaseOutSine().setOnUpdate((float value) =>
        {
            _gameTitleText.fontSize = value;
        }).setOnComplete(() =>
        {
            _3DCanvas.SetActive(false);
            _ourWorldNameCanvasText.GetComponent<TextMeshPro>().text = "OUR WORLD";
        });
    }
    #endregion

    #region Design 2 To 1
    public void Design2To1()
    { 
        ObjectRotationState(false);
        SwapLensFlareSprite(_newLensFlare, false);
        LeanTween.rotateLocal(_sceneHolder, new Vector3(26.7f, 173.5f, -4.36f), 2).setEaseOutSine();
        LeanTween.rotateLocal(_earthModel, new Vector3(0f, 0f, 0f), 3).setEaseOutSine().setOnComplete(() =>
        {
            ChangeEarthRotationAxis(ObjectRotation.AxisToRotateIn.UP);
            ObjectRotationState(true);
        });
        LeanTween.moveLocal(_earth, new Vector3(-0.65f, -0.4f, -1.57f), 3).setEaseOutSine();
        LeanTween.moveLocalZ(_stars, -45, 3).setEaseOutSine();
        LeanTween.moveLocalZ(_starFlare, 0, 3).setEaseOutSine();
        LeanTween.moveLocal(_SunLensFlareHolder, new Vector3(-1.85f, -0.36f, -16f), 3).setEaseOutSine();
        LeanTween.scale(_SunLensFlare1.gameObject, new Vector3(1, 1, 1), 3f).setEaseOutSine();
        LeanTween.scale(_SunLensFlare2.gameObject, new Vector3(1, 1, 1), 3f).setEaseOutSine();
        LeanTween.scale(_SunLensFlare3.gameObject, new Vector3(1, 1, 1), 3f).setEaseOutSine();
        LeanTween.value(22f, 35f, 3).setEaseOutSine().setOnUpdate((float value) =>
        {
            _cam.fieldOfView = value;
        });
        LeanTween.value(1, 0, 3).setEaseOutSine().setOnUpdate((float value) =>
        {
            _introVolume.weight = value;
            _finalVolume.weight = 1 - value;
        });
    }
    #endregion

    #region Design 1 To 2
    public void Design1To2()
    {
        ObjectRotationState(false);
        SwapLensFlareSprite(_introLensFlare, true);
        LeanTween.rotateLocal(_sceneHolder, new Vector3(23f, 179.93f, -4.36f), 2).setEaseOutSine();
        LeanTween.rotateLocal(_earthModel, new Vector3(0f, 0f, 0f), 3).setEaseOutSine();
        LeanTween.moveLocal(_earth, new Vector3(-0.02f, -0.53f, -1.45f), 3).setEaseOutSine();
        LeanTween.moveLocalZ(_stars, -65, 3).setEaseOutSine();
        LeanTween.moveLocalZ(_starFlare, -6.82f, 3).setEaseOutSine();
        LeanTween.moveLocal(_SunLensFlareHolder, new Vector3(0f, 3.9f, -60f), 3).setEaseOutSine();
        LeanTween.scale(_SunLensFlare1.gameObject, new Vector3(1.5f, 1.5f, 1.5f), 3f).setEaseOutSine();
        LeanTween.scale(_SunLensFlare2.gameObject, new Vector3(1.5f, 1.5f, 1.5f), 3f).setEaseOutSine();
        LeanTween.scale(_SunLensFlare3.gameObject, new Vector3(1.5f, 1.5f, 1.5f), 3f).setEaseOutSine();
        LeanTween.value(35f, 22f, 3).setEaseOutSine().setOnUpdate((float value) =>
        {
            _cam.fieldOfView = value;
        });
        LeanTween.value(1, 0, 3).setEaseOutSine().setOnUpdate((float value) =>
        {
            _introVolume.weight = 1 - value;
            _finalVolume.weight = value;
        }).setOnComplete(() => {
            ChangeEarthRotationAxis(ObjectRotation.AxisToRotateIn.RIGHT);
            ObjectRotationState(true);
        });
    }
    #endregion
}