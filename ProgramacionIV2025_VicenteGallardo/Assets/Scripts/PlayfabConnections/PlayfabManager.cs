using PlayFab.ClientModels;
using PlayFab;
using UnityEngine;
using System;
using static UnityEditor.ShaderData;
using UnityEngine.Events;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms.Impl;



public class PlayfabManager : MonoBehaviour
{
    public string userName;
    public string email;
    public string password;

    public delegate void OnEndRequestDel(string msg, bool result);
    OnEndRequestDel OnEndRequestEvent;
    public delegate void OnLoadRequestDel(string data, bool result);
    OnLoadRequestDel OnEndLoadRequestEvent;
    public delegate void OnLoadLeaderBoard(List<LeaderboardData> leaderBoard);
    OnLoadLeaderBoard OnEndLoadLeaderBoardEvent;



    public void LoginAnonymous(OnEndRequestDel onEndRequest)
    {
        OnEndRequestEvent = onEndRequest;

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
            CustomId = System.Guid.NewGuid().ToString(),
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
    }

    public void LoginUser(string email, string password, OnEndRequestDel onEndRequest)
    {
        OnEndRequestEvent = onEndRequest;
        var request = new LoginWithEmailAddressRequest
        {
            Email = email,
            Password = password
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    public void LoginUserName(string userName, string password, OnEndRequestDel onEndRequest)
    {
        OnEndRequestEvent = onEndRequest;
        var request = new LoginWithPlayFabRequest
        {
            Username = userName,
            Password = password
        };

        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnError);
    }

    public void RegisterUser(string userName, string email, string password, OnEndRequestDel onEndRequest)
    {
        OnEndRequestEvent = onEndRequest;
        var request = new RegisterPlayFabUserRequest
        {
            Username = userName,
            Email = email,
            Password = password,
            RequireBothUsernameAndEmail = true
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSucces, OnError);
    }

    public void RecoveryAcount(string email, OnEndRequestDel onEndRequest)
    {
        OnEndRequestEvent = onEndRequest;
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = email,
            TitleId = PlayFabSettings.staticSettings.TitleId

        };

        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnRequestSucces, OnError);
    }

    //eSTA GRABACION NO TIENE AUDIO, OLVIDE LOS HP UWU

    //LO Q VAMOS A HACER ES GUARDAR DATOS LO VAMOS A HACER CON LA MISMA ESTRUCTURA DE SIEMPRE PLOQL
    //Si tiene un action use su action owo
    public void SaveDataInfo(string data, string dataKey, OnEndRequestDel onEndRequest)
    {
        OnEndRequestEvent = onEndRequest;
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { dataKey, data }
            },

            //el primer valor es el nombre del archivo, el segundo la información
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSaved, OnError);
    }

    //Ahora necesitamos cargar la data, es importante crear un nuevo delegado o action
    //

    //Esta funcion fue explicada en clases, por ahora tiene que copiarla y tener fe
    //ahora vamos a probar si funciona nuestro load and save
    public void LoadDataInfo(string dataKey, OnLoadRequestDel onLoadRequestEnd)
    {
        OnEndLoadRequestEvent = onLoadRequestEnd;
        var request = new GetUserDataRequest();

        PlayFabClientAPI.GetUserData(request, result =>
        {
            //El error se debia a que esto tenia un == null, siendo que el valor q nos devuelva si hay data no es null
            if (result.Data != null && result.Data.ContainsKey(dataKey))
            {
                string data = result.Data[dataKey].Value;
                OnEndLoadRequestEvent?.Invoke(data, true);
            }
            else
            {
                Debug.Log("Not key found");
                OnEndLoadRequestEvent?.Invoke(default, false);
            }
        }, OnError);
    }

    public void AddDataToMaxScore(int score, OnEndRequestDel onEndRequest)
    {
        OnEndRequestEvent = onEndRequest;
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new System.Collections.Generic.List<PlayFab.ClientModels.StatisticUpdate>
            {
                new PlayFab.ClientModels.StatisticUpdate
                {
                    StatisticName = "MaxScore", // nombre de tu leaderboard
                    Value = score
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, OnStatisticsResult, OnError);
    }

    public void GetDataFromMaxScore(OnLoadLeaderBoard onLoadLeaderBoard)
    {
        OnEndLoadLeaderBoardEvent = onLoadLeaderBoard;
        var request = new GetLeaderboardRequest
        {
            StatisticName = "MaxScore", // nombre de tu leaderboard
            StartPosition = 0,             // posición inicial (0 = primer lugar)
            MaxResultsCount = 10           // cantidad máxima de resultados
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderBoardLoad, OnError);
    }

    private void OnLeaderBoardLoad(GetLeaderboardResult result)
    {
        List<LeaderboardData> dataList = new List<LeaderboardData>();
        foreach (var item in result.Leaderboard)
        {
            LeaderboardData newData = new LeaderboardData()
            {
                displayName = item.DisplayName,
                score = item.StatValue,
                boardPos = item.Position
            };
            dataList.Add(newData);
        }
        OnEndLoadLeaderBoardEvent?.Invoke(dataList);
    }

    public void SetDisplayName(string displayName, OnEndRequestDel onEndRequest)
    {
        OnEndRequestEvent = onEndRequest;
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = displayName,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnEndRequestDisplayName, OnError);
    }

    private void OnEndRequestDisplayName(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Succes");
        OnEndRequestEvent?.Invoke("Succes", true);
        OnEndRequestEvent = null;
    }

    private void OnStatisticsResult(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Succes");
        OnEndRequestEvent?.Invoke("Succes", true);
        OnEndRequestEvent = null;
    }

    private void OnDataSaved(UpdateUserDataResult result)
    {
        Debug.Log("Succes");
        OnEndRequestEvent?.Invoke("Succes", true);
        OnEndRequestEvent = null;
    }

    private void OnRequestSucces(SendAccountRecoveryEmailResult result)
    {
        Debug.Log("Succes");
        OnEndRequestEvent?.Invoke("Succes", true);
        OnEndRequestEvent = null;
    }

    private void OnRegisterSucces(RegisterPlayFabUserResult result)
    {
        Debug.Log("Succes");
        OnEndRequestEvent?.Invoke("Succes", true);
        OnEndRequestEvent = null;
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Succes");
        OnEndRequestEvent?.Invoke("Succes", true);
        OnEndRequestEvent = null;
    }

    private void OnError(PlayFabError error)
    {
        Debug.Log("Succes");
        OnEndRequestEvent?.Invoke(error.ErrorMessage, false);
        OnEndRequestEvent = null;
    }
}
