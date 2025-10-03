using TMPro;
using UnityEngine;

public class LoginManager : MonoBehaviour
{
    [Header("Clases")]
    [SerializeField] PlayfabLogin playfabLogin;

    [Header("Variables")]
    [SerializeField] GameObject blockPanel;
    [SerializeField] TextMeshProUGUI textFeedback;
    public string mail;
    public string password;


    private void Start()
    {
        SetBlockPanel("Loading...",true);
        playfabLogin.LoginUser(mail,password,OnFinishAction);
    }

    private void SetBlockPanel(string message, bool enable)
    {
        textFeedback.text = message;
        blockPanel.SetActive(enable);
    }

    private void OnFinishAction(string message,bool result)
    {
       if(result)
        {
            SetBlockPanel(message,false);   
        }
        else
        {
            SetBlockPanel(message, true);
        }
    }



}
