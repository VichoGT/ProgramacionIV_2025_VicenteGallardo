using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class LeaderBoardManager : MonoBehaviour
{
    [SerializeField] LeaderBoardContent[] leaderboardContents;
    [SerializeField] CanvasAnimation canvasAnim;
    public int score;

   
    private void Start()
    {
        score = PlayerPrefs.GetInt("CurrentPoints");
        StartCoroutine(TimerLoad());
    }
    IEnumerator TimerLoad()
    {
        yield return canvasAnim.AnimPanelCoroutine(true);
        yield return canvasAnim.ShowPointsCoroutine(score);
        LoadLeaderboard();
        yield return null;
       
    }
    
    

    void LoadLeaderboard() 
    {
        PlayfabManager playfabmanager = new PlayfabManager();
        playfabmanager.GetDataFromMaxScore(SetContent);
    }

    void SetContent(List<LeaderboardData> leaderBoardDataList)
    {
        for (int i = 0; i < leaderboardContents.Length; i++)
        {
            if(i < leaderBoardDataList.Count)
            {
                leaderboardContents[i].gameObject.SetActive(true);
                leaderboardContents[i].SetLeaderBoardContent(leaderBoardDataList[i]);   
            }
            else
            {
                leaderboardContents[i].gameObject.SetActive(false);
            }
        }
    }

   
}
