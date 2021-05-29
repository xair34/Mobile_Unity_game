using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel;

    public void ShowPauseMenuPanel()
    {
        Time.timeScale = 0;
        pauseMenuPanel.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenuPanel.SetActive(false);
    }
    public void GoToMainMenu()
    {
        AudioManager.instance.StopPlayingSound("BattleTheme");
        AudioManager.instance.PlaySound("MainMenuTheme");
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void RestarLevel()
    {
        AudioManager.instance.PlaySound("BattleTheme");
        SceneManager.LoadScene(1);
    }
}
