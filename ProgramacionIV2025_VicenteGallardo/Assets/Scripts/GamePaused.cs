using UnityEngine;

public class GamePaused : MonoBehaviour
{
    public GameObject canvasPanelStore;
    public GameObject canvasPauseGame;
    public bool isGamePaused = false;
    public Shooting shooting;

    public void ResumeGame()
    {
        shooting.enabled = true;
        canvasPanelStore.SetActive(false);
        canvasPauseGame.SetActive(true);

        Time.timeScale = 1;
        isGamePaused = false;
    }
    public void Pause()
    {
        shooting.enabled = false;
        canvasPanelStore.SetActive(true);
        canvasPauseGame.SetActive(false);
        Time.timeScale = 0;
        isGamePaused = true;
    }
}
