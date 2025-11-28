using PlayFab.EventsModels;
using PlayFab;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;
using static MySecondCustomEvent;

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
        if(isInitilized)
        {
            MyFirstCustomEvent myFirstCustomEvent = new MyFirstCustomEvent()
            {
                mFCE_LindoFloat = MFCE_LindoFloat,
            };
             
            AnalyticsService.Instance.RecordEvent(myFirstCustomEvent);
            AnalyticsService.Instance.Flush(); // esto manda el evento altiro
            

        }
    }   public void SaveMySecondCustomEvent(int MSEC_LindoInt,bool MSEC_LindoBool,string MSEC_Lindostring)
    {
        if(isInitilized)
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

    public void LevelStart(int level)
    {
      
    }

    public void LevelComplete(int level, float time)
    {
       
    }

    public void PlayerDied(string reason, float time, Vector3 pos)
    {
        if(isInitilized)
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

    public void ItemCollected(string itemId)
    {
      
    }

   

    public void EnemiesDiesId(string enemies)  // funcion dos
    {
        
    } 
    public void MaxPoint(int points)  // funcion uno
    {
        
    }
  
    // en la nomenclatura de playfab, se usa el guio bajo para separar palabras
}
