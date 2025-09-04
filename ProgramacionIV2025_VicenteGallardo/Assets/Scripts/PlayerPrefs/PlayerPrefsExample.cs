using UnityEngine;


public class PlayerPrefsExample : MonoBehaviour
{
    public string keyName; // nombre del archivo

    public string info; // informacion del archivo

    public Monsita monsita;



    public void SaveData()
    {


         string data = JsonUtility.ToJson(monsita);

        PlayerPrefs.SetString(keyName,data);

        Debug.Log("Datos Guardados...");

    }

    public void GetData() // sirve para cargar datoss
    {

        string data = PlayerPrefs.GetString(keyName, "Null");
        if(data != null )
        {
            print(data);
            monsita = JsonUtility.FromJson<Monsita>(data);
        }

        Debug.Log("Datos Cargados...");

        //info = PlayerPrefs.GetString(keyName);


    }


    

}


[System.Serializable]   // Clase Vacia para el JSON
public class Monsita 
{
    public string name = "Monsa";
    public int lvl = 1;
    public string[] items;



}
