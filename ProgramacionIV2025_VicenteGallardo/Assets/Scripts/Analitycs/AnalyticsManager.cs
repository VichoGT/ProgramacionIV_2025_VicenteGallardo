using PlayFab;
using PlayFab.EventsModels;
using System;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using static MySecondCustomEvent;
using static PlayerDieEvent;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager Instance;
    [HideInInspector] public bool isInitilized = false;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    private async void Start()
    {
        await UnityServices.InitializeAsync();
        AnalyticsService.Instance.StartDataCollection();
        isInitilized = true;
    }



    public void SaveMyFirstCustomEvent(float MFCE_LindoFloat)
    {
        if (isInitilized)
        {
            MyFirstCustomEvent myFirstCustomEvent = new MyFirstCustomEvent()
            {
                mFCE_LindoFloat = MFCE_LindoFloat,
            };

            AnalyticsService.Instance.RecordEvent(myFirstCustomEvent);
            AnalyticsService.Instance.Flush(); // esto manda el evento altiro


        }
    }
    public void SaveMySecondCustomEvent(int MSEC_LindoInt, bool MSEC_LindoBool, string MSEC_Lindostring)
    {
        if (isInitilized)
        {
            MySecondCustomEvent mySecondCustomEvent = new MySecondCustomEvent()
            {
                mSCE_LindoInt = MSEC_LindoInt,
                mSCE_LindoBool = MSEC_LindoBool,
                mSCE_LindoString = MSEC_Lindostring




            };

            AnalyticsService.Instance.RecordEvent(mySecondCustomEvent);
            AnalyticsService.Instance.Flush();


        }
    }




    // -----------------------------
    //   ATAJOS PARA EVENTOS COMUNES
    // -----------------------------


    public void PlayerDied(string reason, float time, Vector3 pos) // funcion uno 
    {
        if (isInitilized)
        {
            PlayerDieEvent playerDieEvent = new PlayerDieEvent()
            {
                PD_Reason = reason,
                PD_Time = time,
                PD_PosX = pos.x,
                PD_PosY = pos.y,
                PD_PosZ = pos.z,


            };
            AnalyticsService.Instance.RecordEvent(playerDieEvent);
            AnalyticsService.Instance.Flush();
        }
    }

    public void EnemyDefeated(string enemies) // funcion dos
    {
        if (isInitilized)
        {

            EnemyDefeatedEvent enemyDefeatedEvent = new EnemyDefeatedEvent()
            {

                EDE_StringEnemy = enemies,

            };
            AnalyticsService.Instance.RecordEvent(enemyDefeatedEvent);
            AnalyticsService.Instance.Flush();

        }

    }
    public void MaxPointsPerGame(int points)  // funcion tres
    {
        if (isInitilized)
        {
            Debug.Log("Estoy Scoreando!");
            MaxPointsPerGameEvent maxPointsPerGameEvent = new MaxPointsPerGameEvent()
            {

                MPPGE_IntScore = points,

            };
            AnalyticsService.Instance.RecordEvent(maxPointsPerGameEvent);
            AnalyticsService.Instance.Flush();

        }

    }

    public void MaxBulletsShoot(int bullets)  // funcion cuatro
    {
        if (isInitilized)
        {

            MaxBulletShootEvent maxBulletShootEvent = new MaxBulletShootEvent()
            {

                MBSE_IntBullet = bullets,

            };
            AnalyticsService.Instance.RecordEvent(maxBulletShootEvent);
            AnalyticsService.Instance.Flush();

        }

    }
    // en la nomenclatura de playfab, se usa el guio bajo para separar palabras
}
