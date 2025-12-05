using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TankGameManager : MonoBehaviour
{
    [SerializeField] Player player;
    public float tiempoInicial = 60f;
    private float tiempoRestante;
    public TextMeshProUGUI textoTimer;

    private bool contando = true;
    float startRealtime;
    float realTime; // tiempo real que duró el jugador

    void Start()
    {
        startRealtime = Time.time;
        tiempoRestante = tiempoInicial;
    }

    void Update()
    {
        if (contando)
        {
            realTime = Time.time - startRealtime;
            tiempoRestante -= Time.deltaTime;

            if (tiempoRestante <= 0)
            {
                tiempoRestante = 0;
                contando = false;
                FinishTimer();
            }

            if (textoTimer != null)
                textoTimer.text = Mathf.CeilToInt(tiempoRestante).ToString();
        }
    }

    public void AddTime(float amount)
    {
        tiempoRestante += amount;
    }

    void FinishTimer()
    {
        GameOver();
        OnEndSaveScore("GameOver", true);

        AnalyticsManager.Instance.PlayerDied("Time Finished", realTime, player.transform.position);
        Debug.Log("¡Tiempo finalizado!");
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
        SceneManager.LoadScene(2); // escena GameOver
    }
}

