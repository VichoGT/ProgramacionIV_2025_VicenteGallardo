using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TankGameManager : MonoBehaviour
{
   [SerializeField] Player player;
    public float tiempoInicial = 60f; 
    private float tiempoRestante;
    public TextMeshProUGUI textoTimer;
    //Faltaria solo Hacer La condicion de victoria o derrota 
    private bool contando = true;
    float startRealtime;
    float realTime; // es para saber cual es el tiempo real que duro el pj
    // Crear un real tie
    void Start()
    {
        startRealtime = Time.time;  // esto

        tiempoRestante = tiempoInicial;
    }
    void Update()
    {
        if (contando)
        {
            realTime = Time.time - startRealtime; // esto 
            // Resta el tiempo que ha pasado desde el último frame
            tiempoRestante -= Time.deltaTime;

            // Evita que sea menor a 0
            if (tiempoRestante <= 0)
            {              
                tiempoRestante = 0;
                contando = false;
                FinishTimer();
               
            }

            // Muestra el tiempo redondeado
            if (textoTimer != null)
                textoTimer.text = Mathf.CeilToInt(tiempoRestante).ToString();
        }
    }
    void FinishTimer()
    {
        GameOver();
        OnEndSaveScore("GameOver", true);
        Debug.Log("¡Tiempo finalizado!");
        // Aquí puedes poner lo que pase cuando el tiempo llegue a 0
    }
    void GameOver()
    {
        player.score = PlayerScoreManager.Instance.score;
        player.SaveData();
        PlayerScoreManager.Instance.SaveDataToLeaderBoard(OnEndSaveScore);
        AnalyticsManager.Instance.PlayerDied("Time Finished", realTime, player.transform.position);
    }

    private void OnEndSaveScore(string msg, bool result)
    {
        SceneManager.LoadScene(2);       
    }
}
