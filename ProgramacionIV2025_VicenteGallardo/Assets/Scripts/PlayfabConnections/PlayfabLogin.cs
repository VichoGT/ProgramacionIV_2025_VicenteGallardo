using PlayFab.ClientModels;
using PlayFab;
using UnityEngine;
using System;
using static UnityEditor.ShaderData;
using UnityEngine.Events;


public class PlayfabLogin : MonoBehaviour
{
    private Action<string,bool> OnFinishActionEvent;

    public void LoginAnonymous(Action<string, bool> onFinishAction)
    {
        OnFinishActionEvent = onFinishAction;
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            /*
            Please change the titleId below to your own titleId from PlayFab Game Manager.
            If you have already set the value in the Editor Extensions, this can be skipped.
            */
            PlayFabSettings.staticSettings.TitleId = "157271";
        }
        var request = new LoginWithCustomIDRequest
        {
            CustomId = "VicenteGallardo",
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }
    public void RegisterUser(string mail, string pass, Action<string,bool> onFinishAction)
    {
        OnFinishActionEvent = onFinishAction;
        var request = new RegisterPlayFabUserRequest
        {
            Email = mail,
            Password = pass,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterUserResult, OnError);
    }

    public void LoginUser(string mail, string pass,Action<string,bool> onFinishAction)
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = mail,
            Password = pass,        
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginResult, OnError);
     
    }

    private void OnLoginResult(LoginResult result)
    {
        Debug.Log("Te has logeado!");
        OnFinishActionEvent?.Invoke("Success", true);
    }

    private void OnError(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
        OnFinishActionEvent?.Invoke(error.GenerateErrorReport(), false);
    }

    private void OnRegisterUserResult(RegisterPlayFabUserResult result)
    {
        OnFinishActionEvent?.Invoke("Success",true);
        Debug.Log("Has Registrado tu usuario Correctamente");
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        OnFinishActionEvent?.Invoke("Success", true);
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.ErrorMessage.ToString());
        OnFinishActionEvent?.Invoke(error.GenerateErrorReport(),false);
    }
}
