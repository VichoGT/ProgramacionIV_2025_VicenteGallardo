using UnityEngine;

public class LoadSaveSystem
{
    string playerinfodDataKey = "PlayerInfo"; // nombre Archivo

    public PlayerDataInfo LoadPlayerInfo(System.Action<PlayerDataInfo> onEndLoadData)
    {
        string json = PlayerPrefs.GetString(playerinfodDataKey);
       
        PlayerDataInfo loadData = JsonUtility.FromJson<PlayerDataInfo>(json);

        PlayfabManager playfab = new PlayfabManager();

        playfab.LoadDataInfo(playerinfodDataKey, (data, result) =>
        {
            if(result == true)
            {
                json = data;
                PlayerDataInfo loadData = JsonUtility.FromJson<PlayerDataInfo>(json);
                onEndLoadData(loadData);
            }
        });
        return loadData;
    }


    public void SavePlayerInfo(PlayerDataInfo dataToSave)
    {
        string json = JsonUtility.ToJson(dataToSave);
        PlayerPrefs.SetString(playerinfodDataKey, json);
        PlayfabManager playfab = new PlayfabManager();
        playfab.SaveDataInfo(json,playerinfodDataKey,null);
        Debug.Log("Save Succes");
    }


}
