using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
public class LoginManager : MonoBehaviour
{
    [SerializeField] PlayfabManager playfabManager;
    [SerializeField] GameObject panelBlock;
    [SerializeField] TMPro.TextMeshProUGUI textFeedback;
    [SerializeField] GameObject[] panels;

    public string userName;
    public string password;
    public string repeatPassword;
    public string email;
    public List<LeaderboardData> leaderBoard;

    //Si se dan cuenta estos son dos ints
    //Ahora la pregunta es como convierto dos valores a un solo texto
    //La respuesta es con JSON, Por lo cual tenemos que crear una clase empty para almacenar nuestra data
    public int score;
    public int lifePoints;
    LoginPanelType currentPanel;

    private void Start()
    {
        playfabManager = new PlayfabManager();
        SetPanels(LoginPanelType.Login);
    }


    private void OnEndRequest(string msg, bool result)
    {
        if (result == true)
        {
            SetBlockPanel("Succes", false);
            switch (currentPanel)
            {
                case LoginPanelType.Login:
                    SceneManager.LoadScene(1);
                    break;
                case LoginPanelType.Register:
                    SceneManager.LoadScene(1);
                    break;
                case LoginPanelType.Recovery:
                    SetPanels(LoginPanelType.Login);
                    break;

            }
        }
        else
        {
            SetBlockPanel(msg, true);
        }
    }

    void SetBlockPanel(string msg, bool enable)
    {
        textFeedback.text = msg;
        panelBlock.SetActive(enable);
    }

    void SetPanels(LoginPanelType panelType)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (i == (int)panelType)
            {
                panels[i].SetActive(true);
            }
            else panels[i].SetActive(false);
        }
        currentPanel = panelType;
    }

    public void ChangeUserName(string val)
    {
        userName = val;
    }

    public void ChangeEmail(string val)
    {
        email = val;
    }

    public void ChangePassword(string val)
    {
        password = val;
    }

    public void ChangeRepeatPassword(string val)
    {
        repeatPassword = val;
    }

    public void LoginButton()
    {
        SetBlockPanel("Loading", true);
        playfabManager.LoginUserName(userName, password, OnEndRequest);
       

    }

    public void CreateAcountButton()
    {
        SetPanels(LoginPanelType.Register);
    }

    public void CreateAcountCreateButton()
    {
        if (password == repeatPassword)
        {
            SetBlockPanel("Creating...", true);
            playfabManager.LoginAnonymous(null);
            playfabManager.RegisterUser(userName, email, password, OnEndRequest);
        }
        else
        {
            Debug.Log("las claves deben coincidir");
        }
    }

    public void RecoveryAcountButton()
    {
        SetBlockPanel("Sending Email...", true);
        playfabManager.RecoveryAcount(email, OnEndRequest);
    }

    public void GoToRecoveryAcountButton()
    {
        SetPanels(LoginPanelType.Recovery);
    }

    public void GoBackButton()
    {
        SetPanels(LoginPanelType.Login);
    }
}

public enum LoginPanelType
{
    Login,
    Register,
    Recovery
}

//No olvidar agregar algo importante
[System.Serializable]
public class PJData
{
    public int lifePoints;
    public int score;
}
//esta clase la volveremos un JSON

[System.Serializable]
public class LeaderboardData
{
    public string displayName;
    public int score;
    public int boardPos;
}
//esta clase la volveremos un JSON
