using UnityEngine;

public class LoadSaveSystem
{
    string playerinfodDataKey = "PlayerInfo";


    public PlayerDataInfo LoadPlayerInfo()
    {
        string json = PlayerPrefs.GetString(playerinfodDataKey);
       

        PlayerDataInfo loadData = JsonUtility.FromJson<PlayerDataInfo>(json);
        return loadData;
    }


    public void SavePlayerInfo(PlayerDataInfo dataToSave)
    {
        string json = JsonUtility.ToJson(dataToSave);
        PlayerPrefs.SetString(playerinfodDataKey, json);
        Debug.Log("Save Succes");
    }


}
