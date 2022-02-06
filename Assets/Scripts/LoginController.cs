using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    [Header("Login")]
    public GameObject entrancePanel;
    public GameObject loginPanel;
    public InputField loginInp;
    public InputField passwordInp;
    public Button loginBtn;
    public Button playBtn;
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
    public GameObject gamePanel;

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

        MenuActive(entrancePanel);

        playBtn.onClick.AddListener(ButtonPlay);
        loginBtn.onClick.AddListener(ButtonLogin);
        registerBtn.onClick.AddListener(ButtonRegister);
        backBtnReg.onClick.AddListener(ButtonBackReg);
        messageBtn.onClick.AddListener(ButtonMessageClose);
        registerBtnReg.onClick.AddListener(ButtonRegisterReg);
    }

    #region ##### FUNCTIONS #####
    void MenuActive(GameObject panel)
    {
        entrancePanel.gameObject.SetActive(entrancePanel.name.Equals(panel.name));
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
        loginPanel.gameObject.SetActive(false);
        gamePanel.SetActive(true);
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

    void ButtonPlay()
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
            messageTxt.text = "A senha não confere!";
            err = true;
        }

        if (err)
        {
            messagePanel.gameObject.SetActive(true);
            return;
        }

        RegisterSend(emailTemp, passTemp);
        MenuActive(gamePanel);
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