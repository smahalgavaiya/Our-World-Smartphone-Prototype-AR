using SimpleJSON;
using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Authentication : MonoBehaviour
{
    private const string OASIS_REGISTER_AVATAR = "https://api.oasisplatform.world/api/avatar/register";
    private const string OASIS_GET_TERMS = "https://api.oasisplatform.world/api/avatar/GetTerms";
    private const string OASIS_AUTHENTICATE = "https://api.oasisplatform.world/api/avatar/authenticate";
    private const string OASIS_AUTOLOGIN = "https://api.oasisplatform.world/api/Avatar/GetAvatarByJwt";

    [Header("Authentication")]
    public TMP_InputField _signUpFirstName;
    public TMP_InputField _signUpLastName;
    public TMP_InputField _signUpEmail;
    public TMP_InputField _signUpPassword;
    public TMP_InputField _signUpConfirmPassword;
    public TMP_InputField _signInEmail;
    public TMP_InputField _signInPassword;
    public TextMeshProUGUI _termsText;
    public Toggle _termsToggle;
    public Toggle _termsAgree;
    public Button _signUpContinue;
    public GameObject _signUpInHolder;
    public GameObject _termsHolder;
    public GameObject _signInPanel;
    public GameObject _signUpPanel;
    public TextMeshProUGUI _firstNameWarning;
    public TextMeshProUGUI _lastNameWarning;
    public TextMeshProUGUI _emailWarning;
    public TextMeshProUGUI _passwordWarning;
    public TextMeshProUGUI _confirmPasswordWarning;
    public TextMeshProUGUI _signInEmailWarning;
    public TextMeshProUGUI _signInPasswordWarning;

    [Header("Warning/Info")]
    public GameObject _warningInfoHolder;
    public GameObject _authenticationHolder;
    public GameObject _laodingText;
    public GameObject _loadingHolder;
    public GameObject _infoHolder;
    public TextMeshProUGUI _3BEText;
    public TextMeshProUGUI _headerText;
    public TextMeshProUGUI _bodyText;

    private struct RegisterData
    {
        public string title;
        public string firstName;
        public string lastName;
        public string avatarType;
        public string email;
        public string password;
        public string confirmPassword;
        public int createdOASISType;
        public bool acceptTerms;
    }
    private struct AuthenticateData
    {
        public string email;
        public string password;
    }
    private enum ShowWarning
    {
        SignUpFail,
        SignUpSuccess,
        SignInFail,
        SignInSuccess
    }

    #region UI/UX
    public void ShowSignUpPanel(bool value)
    {
        if (value)
        {
            _firstNameWarning.gameObject.SetActive(false);
            _lastNameWarning.gameObject.SetActive(false);
            _emailWarning.gameObject.SetActive(false);
            _passwordWarning.gameObject.SetActive(false);
            _confirmPasswordWarning.gameObject.SetActive(false);
            _signUpFirstName.text = "";
            _signUpLastName.text = "";
            _signUpEmail.text = "";
            _signUpPassword.text = "";
            _signUpConfirmPassword.text = "";
            _termsToggle.isOn = false;
            _termsAgree.isOn = false;
            LeanTween.rotateY(_signInPanel, 90, 0.5f).setEaseInSine().setOnComplete(() =>
            {
                _signInPanel.SetActive(false);
                _signUpPanel.transform.localEulerAngles = new Vector3(0, -90, 0);
                _signUpPanel.SetActive(true);
                LeanTween.rotateY(_signUpPanel, 0, 0.5f).setEaseOutSine();
            });
        }
        else
        {
            _signInEmailWarning.gameObject.SetActive(false);
            _signInPasswordWarning.gameObject.SetActive(false);
            _signInEmail.text = "";
            _signInPassword.text = "";
            LeanTween.rotateY(_signUpPanel, 90, 0.5f).setEaseInSine().setOnComplete(() =>
            {
                _signUpPanel.SetActive(false);
                _signInPanel.transform.localEulerAngles = new Vector3(0, -90, 0);
                _signInPanel.SetActive(true);
                LeanTween.rotateY(_signInPanel, 0, 0.5f).setEaseOutSine();
            });
        }
    }
    public void ShowTerms(bool value)
    {
        _signUpInHolder.SetActive(!value);
        _termsHolder.SetActive(value);
    }
    public void TermsToggle(bool value)
    {
        if (value)
            _signUpContinue.interactable = true;
        else
            _signUpContinue.interactable = false;
    }
    public void TermScrollRectValue(Vector2 value)
    {
        if (value.y < 0.01)
            _termsAgree.interactable = true;
    }
    public void AgreeToTerms(bool value)
    {
        if (value)
        {
            ShowTerms(false);
            _termsToggle.interactable = true;
            _termsToggle.isOn = true;
        }
        else
        {
            _termsToggle.interactable = false;
            _termsToggle.isOn = false;
        }
    }
    #endregion
    #region Warning/Info
    private void ShowWarningInfoPanel(bool value)
    {
        _warningInfoHolder.SetActive(value);
        _authenticationHolder.SetActive(!value);
    }
    private void SetInfo(ShowWarning value, string body = "")
    {
        SetLogoTop();
        if (value == ShowWarning.SignUpFail)
        {
            _headerText.text = "Fail";
            _bodyText.text = body;
        }
        else if (value == ShowWarning.SignUpSuccess)
        {
            _headerText.text = "Success";
            _bodyText.text = body;
        }
        else if (value == ShowWarning.SignInFail)
        {
            _headerText.text = "Fail";
            _bodyText.text = body;
        }
        else if (value == ShowWarning.SignInSuccess)
        {
            LeanTween.cancelAll();
            SceneManager.LoadScene("Menu 3D");
        }

        LeanTween.value(0, 1, 0.5f).setEaseOutSine().setOnComplete(() =>
        {
            _infoHolder.SetActive(true);
            LeanTween.value(0, 1, 0.5f).setEaseOutSine().setOnUpdate((float value) =>
            {
                _headerText.alpha = value;
                _bodyText.alpha = value;
            });
        });
    }
    private void SetLogoTop()
    {
        _laodingText.SetActive(false);
        LeanTween.moveLocalY(_loadingHolder, 225, 1).setEaseOutSine();
        LeanTween.value(250, 200, 1).setEaseOutSine().setOnUpdate((float value) => _3BEText.fontSize = value);
    }
    public void Continue()
    {
        _laodingText.SetActive(true);
        _loadingHolder.transform.localPosition = new Vector3(0, 0, 0);
        _3BEText.fontSize = 250;
        _infoHolder.SetActive(false);
        _headerText.alpha = 0;
        _bodyText.alpha = 0;
        ShowWarningInfoPanel(false);
    }
    #endregion

    private void Start()
    {
        AutoLogin();
    }

    private void AutoLogin()
    {
        if (PlayerPrefs.GetString("JWTToken", "") != "")
            StartCoroutine(LoginWithJWT(PlayerPrefs.GetString("JWTToken")));
        else
            GetTerms();
    }

    public IEnumerator LoginWithJWT(string jwt)
    {
        using var request = new UnityWebRequest(OASIS_AUTOLOGIN);
        request.method = UnityWebRequest.kHttpVerbGET;
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", $"Bearer {jwt}");
        ShowWarningInfoPanel(true);
        yield return request.SendWebRequest();

        JSONNode data = JSON.Parse(request.downloadHandler.text);
        if (data["isError"].Value == "true")
            SetInfo(ShowWarning.SignInFail, data["message"].Value);
        else
        {
            SetInfo(ShowWarning.SignInSuccess);
        }
    }

    private IEnumerator GetTerms()
    {
        using var request = UnityWebRequest.Get(OASIS_GET_TERMS);
        yield return request.SendWebRequest();

        JSONNode data = JSON.Parse(request.downloadHandler.text);
        if (request.result != UnityWebRequest.Result.Success)
            Debug.Log(request.error);
        else
            _termsText.text = data["result"].Value;
    }

    //Sign Up
    public void SignUp() => CheckInformationInSignUpPage();
    private void CheckInformationInSignUpPage()
    {
        bool errorsInInput = false;
        var checkEmailRegex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            RegexOptions.CultureInvariant | RegexOptions.Singleline);

        if (string.IsNullOrWhiteSpace(_signUpFirstName.text))
        {
            _firstNameWarning.gameObject.SetActive(true);
            errorsInInput = true;
        }
        else
            _firstNameWarning.gameObject.SetActive(false);

        if (string.IsNullOrWhiteSpace(_signUpLastName.text))
        {
            _lastNameWarning.gameObject.SetActive(true);
            errorsInInput = true;
        }
        else
            _lastNameWarning.gameObject.SetActive(false);

        if (string.IsNullOrWhiteSpace(_signUpEmail.text) || !checkEmailRegex.IsMatch(_signUpEmail.text))
        {
            _emailWarning.gameObject.SetActive(true);
            errorsInInput = true;
        }
        else
            _emailWarning.gameObject.SetActive(false);

        if (string.IsNullOrWhiteSpace(_signUpPassword.text) || _signUpPassword.text.Length < 6)
        {
            _passwordWarning.gameObject.SetActive(true);
            errorsInInput = true;
        }
        else
            _passwordWarning.gameObject.SetActive(false);

        if (string.IsNullOrWhiteSpace(_signUpConfirmPassword.text) || _signUpConfirmPassword.text != _signUpPassword.text)
        {
            _confirmPasswordWarning.gameObject.SetActive(true);
            errorsInInput = true;
        }
        else
            _confirmPasswordWarning.gameObject.SetActive(false);

        if (errorsInInput)
            return;

        StartCoroutine(SignUpRequest());
    }
    private IEnumerator SignUpRequest()
    {
        byte[] registerData = GetRegisterDataJsonBytes();
        using var request = new UnityWebRequest(OASIS_REGISTER_AVATAR);
        request.method = UnityWebRequest.kHttpVerbPOST;
        request.uploadHandler = new UploadHandlerRaw(registerData);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        ShowWarningInfoPanel(true);
        yield return request.SendWebRequest();

        JSONNode data = JSON.Parse(request.downloadHandler.text);
        if (data["status"].Value.StartsWith("4") || data["status"].Value.StartsWith("4"))
            SetInfo(ShowWarning.SignUpFail, data["title"].Value);
        else
            SetInfo(ShowWarning.SignUpSuccess, data["message"].Value);
    }
    private byte[] GetRegisterDataJsonBytes()
    {
        var registerData = new RegisterData
        {
            title = "",
            firstName = _signUpFirstName.text,
            lastName = _signUpLastName.text,
            avatarType = "User",
            email = _signUpEmail.text,
            password = _signUpPassword.text,
            confirmPassword = _signUpConfirmPassword.text,
            createdOASISType = 7,
            acceptTerms = _termsToggle.isOn
        };
        return System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(registerData));
    }

    //Sign In
    public void SignIn() => CheckInformationSignInPage();
    private void CheckInformationSignInPage()
    {
        bool errorsInInput = false;
        var checkEmailRegex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            RegexOptions.CultureInvariant | RegexOptions.Singleline);

        if (string.IsNullOrWhiteSpace(_signInEmail.text) || !checkEmailRegex.IsMatch(_signInEmail.text))
        {
            _signInEmailWarning.gameObject.SetActive(true);
            errorsInInput = true;
        }
        else
            _signInEmailWarning.gameObject.SetActive(false);

        if (string.IsNullOrWhiteSpace(_signInPassword.text) || _signInPassword.text.Length < 6)
        {
            _signInPasswordWarning.gameObject.SetActive(true);
            errorsInInput = true;
        }
        else
            _signInPasswordWarning.gameObject.SetActive(false);

        if (errorsInInput)
            return;

        StartCoroutine(SignInRequest());
    }
    private IEnumerator SignInRequest()
    {
        byte[] authenticateData = GetAuthenticateDataJsonByte();
        using var request = new UnityWebRequest(OASIS_AUTHENTICATE);
        request.method = UnityWebRequest.kHttpVerbPOST;
        request.uploadHandler = new UploadHandlerRaw(authenticateData);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        ShowWarningInfoPanel(true);
        yield return request.SendWebRequest();

        JSONNode data = JSON.Parse(request.downloadHandler.text);
        if (data["isError"].Value == "true")
            SetInfo(ShowWarning.SignInFail, data["message"].Value);
        else
        {
            PlayerPrefs.SetString("JWTToken", data["result"]["avatar"]["jwtToken"].Value);
            AvatarInfoManager.Instance.SetAvatarNameAndLevel(data["result"]["avatar"]["fullName"].Value, data["result"]["avatar"]["level"].Value, data["result"]["avatar"]["jwtToken"].Value, data["result"]["avatar"]["avatarId"].Value);
            SetInfo(ShowWarning.SignInSuccess);
        }
    }
    private byte[] GetAuthenticateDataJsonByte()
    {
        var authenticateData = new AuthenticateData
        {
            email = _signInEmail.text,
            password = _signInPassword.text
        };
        return System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(authenticateData));
    }
}
