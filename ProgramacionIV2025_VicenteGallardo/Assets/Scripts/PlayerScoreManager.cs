using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScoreManager : MonoBehaviour
{
    public static PlayerScoreManager Instance;
    public int score = 0;
    public TextMeshProUGUI scoreText;
   
    void Awake()
    {
        Instance = this;
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }

    public void SaveDataToLeaderBoard(PlayfabManager.OnEndRequestDel onEndSave)
    {
        PlayfabManager playfabManager = new PlayfabManager();
        playfabManager.AddDataToMaxScore(score, onEndSave);
        PlayerPrefs.SetInt("CurrentPoints", score);
    }

    
}
