using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Authentication : MonoBehaviour
{
    private const string OASIS_REGISTER_AVATAR = "https://api.oasisplatform.world/api/avatar/register";
    private const string OASIS_GET_TERMS = "https://api.oasisplatform.world/api/avatar/GetTerms";
    private const string OASIS_AUTHENTICATE = "https://api.oasisplatform.world/api/avatar/authenticate";

    public TMP_InputField _signUpFirstName;
    public TMP_InputField _signUpLastName;
    public TMP_InputField _signUpEmail;
    public TMP_InputField _signUpPassword;
    public TMP_InputField _signUpConfirmPassword;
    public TMP_InputField _signInEmail;
    public TMP_InputField _signInPassword;
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
            LeanTween.rotateY(_signInPanel, 90, 0.5f).setEaseInSine().setOnComplete(() => {
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
            LeanTween.rotateY(_signUpPanel, 90, 0.5f).setEaseInSine().setOnComplete(() => {
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

    private void Start()
    {
        //StartCoroutine(GetTerms());
    }
    private IEnumerator GetTerms()
    {
        using (UnityWebRequest register = UnityWebRequest.Get(OASIS_GET_TERMS))
        {
            yield return register.SendWebRequest();

            if (register.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(register.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }

    //Sign Up
    public void SignUp()
    {
        CheckInformationInSignUpPage();
    }
    private void CheckInformationInSignUpPage()
    {
        bool errosInInput = false;
        Regex checkEmailRegex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            RegexOptions.CultureInvariant | RegexOptions.Singleline);

        if (string.IsNullOrWhiteSpace(_signUpFirstName.text))
        {
            _firstNameWarning.gameObject.SetActive(true);
            errosInInput = true;
        }
        else
            _firstNameWarning.gameObject.SetActive(false);

        if (string.IsNullOrWhiteSpace(_signUpLastName.text))
        {
            _lastNameWarning.gameObject.SetActive(true);
            errosInInput = true;
        }
        else
            _lastNameWarning.gameObject.SetActive(false);

        if (string.IsNullOrWhiteSpace(_signUpEmail.text) || !checkEmailRegex.IsMatch(_signUpEmail.text))
        {
            _emailWarning.gameObject.SetActive(true);
            errosInInput = true;
        }
        else
            _emailWarning.gameObject.SetActive(false);

        if (string.IsNullOrWhiteSpace(_signUpPassword.text) || _signUpPassword.text.Length < 6)
        {
            _passwordWarning.gameObject.SetActive(true);
            errosInInput = true;
        }
        else
            _passwordWarning.gameObject.SetActive(false);

        if (string.IsNullOrWhiteSpace(_signUpConfirmPassword.text) || _signUpConfirmPassword.text != _signUpPassword.text)
        {
            _confirmPasswordWarning.gameObject.SetActive(true);
            errosInInput = true;
        }
        else
            _confirmPasswordWarning.gameObject.SetActive(false);

        if (errosInInput)
            return;

        StartCoroutine(SignUpRequest());
    }

    private IEnumerator SignUpRequest()
    {
        string registerData = GetRegisterDataJson();
        Debug.Log(registerData);
        using (UnityWebRequest register = UnityWebRequest.Post(OASIS_REGISTER_AVATAR, registerData))
        {
            register.SetRequestHeader("Content-Type", "application/json");
            yield return register.SendWebRequest();

            if (register.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(register.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }

    private string GetRegisterDataJson()
    {
        RegisterData registerData = new RegisterData();
        registerData.title = "";
        registerData.firstName = _signUpFirstName.text;
        registerData.lastName = _signUpLastName.text;
        registerData.avatarType = "User";
        registerData.email = _signUpEmail.text;
        registerData.password = _signUpPassword.text;
        registerData.confirmPassword = _signUpConfirmPassword.text;
        registerData.createdOASISType = 7;
        registerData.acceptTerms = _termsToggle.isOn;
        return JsonUtility.ToJson(registerData);
    }

    //Sign In
    public void SignIn()
    {
        CheckInformationSignInPage();
    }
    private void CheckInformationSignInPage()
    {
        bool errosInInput = false;
        Regex checkEmailRegex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            RegexOptions.CultureInvariant | RegexOptions.Singleline);

        if (string.IsNullOrWhiteSpace(_signInEmail.text) || !checkEmailRegex.IsMatch(_signInEmail.text))
        {
            _signInEmailWarning.gameObject.SetActive(true);
            errosInInput = true;
        }
        else
            _signInEmailWarning.gameObject.SetActive(false);

        if (string.IsNullOrWhiteSpace(_signInPassword.text) || _signInPassword.text.Length < 6)
        {
            _signInPasswordWarning.gameObject.SetActive(true);
            errosInInput = true;
        }
        else
            _signInPasswordWarning.gameObject.SetActive(false);

        if (errosInInput)
            return;

        StartCoroutine(SignInRequest());
    }

    private IEnumerator SignInRequest()
    {
        string authenticateData = GetAuthenticateDataJson();
        using (UnityWebRequest register = UnityWebRequest.Post(OASIS_AUTHENTICATE, authenticateData))
        {
            register.SetRequestHeader("Content-Type", "application/json");
            yield return register.SendWebRequest();

            if (register.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(register.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }

    private string GetAuthenticateDataJson()
    {
        AuthenticateData authenticateData = new AuthenticateData();
        authenticateData.email = _signInEmail.text;
        authenticateData.password = _signInPassword.text;
        return JsonUtility.ToJson(authenticateData);
    }
}
