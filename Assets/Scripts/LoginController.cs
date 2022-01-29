using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using UnityEngine.SceneManagement;

public class LoginController : MonoBehaviour
{
    [Header("Login")]
    public GameObject loginPanel;
    public InputField loginInp;
    public InputField passwordInp;
    public Button loginBtn;
    public Button registerBtn;

    [Space]
    [Header("Register")]
    public GameObject registerPanel;
    public InputField loginInpReg;
    public InputField passwordInpReg;
    public InputField confPassInpReg;
    public Button registerBtnReg;
    public Button backBtnReg;

    [Space]
    [Header("Message")]
    public GameObject messagePanel;
    public Text messageTxt;
    public Button messageBtn;

    [Space]
    [Header("Rest")]
    public RestController restScritp;

    //public static PlayGamesPlatform platform;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("PLAYER_EMAIL") != "" && PlayerPrefs.GetString("PLAYER_PASSWORD") != "")
        {
            loginInp.text = PlayerPrefs.GetString("PLAYER_EMAIL");
            passwordInp.text = PlayerPrefs.GetString("PLAYER_PASSWORD");
            //ButtonLogin();
        }

        MenuActive(loginPanel);

        loginBtn.onClick.AddListener(ButtonLogin);
        registerBtn.onClick.AddListener(ButtonRegister);
        backBtnReg.onClick.AddListener(ButtonBackReg);
        messageBtn.onClick.AddListener(ButtonMessageClose);
        registerBtnReg.onClick.AddListener(ButtonRegisterReg);

        //if(platform == null)
        //{
        //    PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        //    PlayGamesPlatform.InitializeInstance(config);
        //    PlaGamesPlatform.DebugLogEnabled = true;

        //    platform = PlayGamesPlatform.Activate();
        //}

        //social.active.localuser.authenticate((bool success, string err) =>
        //{
        //    if (success)
        //    {
        //        scenemanager.loadscene(1);
        //    }
        //    else if (!success)
        //    {
        //        logerr.text = "error: " + err;
        //    }
        //});
    }

    #region ##### FUNCTIONS #####
    void MenuActive(GameObject panel)
    {
        loginPanel.gameObject.SetActive(loginPanel.name.Equals(panel.name));
        registerPanel.gameObject.SetActive(registerPanel.name.Equals(panel.name));
        messagePanel.gameObject.SetActive(messagePanel.name.Equals(panel.name));
    }

    void GetMessage(Message pMsg)
    {
        if (pMsg.GetMessage() != "")
        {
            messageTxt.text = pMsg.GetMessage();
            messagePanel.gameObject.SetActive(true);
            return;
        }

        StartGame();
    }

    void StartGame()
    {
        if(loginInp.text != "" && passwordInp.text != "")
        {
            PlayerPrefs.SetString("PLAYER_EMAIL", loginInp.text);
            PlayerPrefs.SetString("PLAYER_PASSWORD", passwordInp.text);
        }
    }

    #endregion

    #region ##### LOGIN #####

    void ButtonLogin()
    {
        bool err = false;

        string emailTemp = loginInp.text;
        string passTemp = passwordInp.text;

        if(emailTemp == "" && !err)
        {
            messageTxt.text = "Digite um email!";
            err = true;
        }
        
        if(passTemp == "" && !err)
        {
            messageTxt.text = "Digite uma senha!";
            err = true;
        }
        
        if (err)
        {
            messagePanel.gameObject.SetActive(true);
            return;
        }

        LoginSend(emailTemp, passTemp);
    }

    void ButtonRegister()
    {
        MenuActive(registerPanel);
    }
    
    void ButtonBackReg()
    {
        MenuActive(loginPanel);
    }
    
    #endregion

    #region ##### REGISTER #####

    void ButtonRegisterReg()
    {
        bool err = false;

        string emailTemp = loginInpReg.text;
        string passTemp = passwordInpReg.text;
        string passTempConf = confPassInpReg.text;

        if (emailTemp == "" && !err)
        {
            messageTxt.text = "Digite um email!";
            err = true;
        }

        if (passTemp == "" && !err)
        {
            messageTxt.text = "Digite uma senha!";
            err = true;
        }

        if (passTempConf == "" && !err)
        {
            messageTxt.text = "Digite a senha novamente!";
            err = true;
        }

        if (passTempConf != passTemp && !err)
        {
            messageTxt.text = "A senha n�o confere!";
            err = true;
        }

        if (err)
        {
            messagePanel.gameObject.SetActive(true);
            return;
        }

        RegisterSend(emailTemp, passTemp);
    }
    
    #endregion

    #region ##### MESSAGE #####

    void ButtonMessageClose()
    {
        messagePanel.gameObject.SetActive(false);
    }

    #endregion

    #region ##### REST FUNCTIONS #####

    void LoginSend(string pEmail, string pPassword)
    {
        restScritp.SendRestGetLogin(pEmail, pPassword, GetMessage);
    }
    
    void RegisterSend(string pEmail, string pPassword)
    {
        restScritp.SendRestPostRegister(pEmail, pPassword, GetMessage);
    }

    #endregion
}

[System.Serializable]
public class Login
{
    public string email;
    public string password;

    public Login(string pEmail, string pPassword)
    {
        this.email = pEmail;
        this.password = pPassword;
    }

    public string Email
    {
        get { return email; }
        set { email = value; }
    }
    
    public string Password
    {
        get { return password; }
        set { password = value; }
    }
}

[System.Serializable]
public class Message
{
    public int status;
    public string message;
    string messageDefault = "Ocorreu um problema, tente novamente em alguns minutos!";

    public Message()
    {

    }

    public Message(int pStatus, string pMessage)
    {
        this.status = pStatus;
        this.message = pMessage;
    }

    public string GetMessage()
    {
        if(this.message == "" && this.status != 200)
        {
            return messageDefault;
        }

        return this.message;
    }
}