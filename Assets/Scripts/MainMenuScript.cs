using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;

    public Toggle musicOn;
    public Toggle musicOff;
    public Toggle soundOn;
    public Toggle soundOff;

    private void Start()
    {
        // set default state of music and sound to true on first time open
        if(PlayerPrefs.GetInt("Default") == 0)
        {
            PlayerPrefs.SetInt("Sound", 1);
            PlayerPrefs.SetInt("Music", 1);
            // disable default reset
            PlayerPrefs.SetInt("Default", 1);
        }
        bool soundIsOn = (PlayerPrefs.GetInt("Sound") == 1) ? true : false;
        bool musicIsOn = (PlayerPrefs.GetInt("Music") == 1) ? true : false;

        musicOn.isOn = musicIsOn;
        musicOff.isOn = !musicIsOn;
        soundOn.isOn = soundIsOn;
        soundOff.isOn = !soundIsOn;
    }
    public void ShowMainMenuPanel()
    {
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    public void ShowSettingsPanel()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void NextSceneLoad()
    {
        AudioManager.instance.StopPlayingSound("MainMenuTheme");
        AudioManager.instance.PlaySound("BattleTheme");
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    public void MuteSound()
    {
        PlayerPrefs.SetInt("Sound", 0);
        AudioListener.pause = true;
    }
    public void MuteMusic()
    {
        PlayerPrefs.SetInt("Music", 0);
        AudioManager.instance.sounds[0].source.mute = true;
        AudioManager.instance.sounds[4].source.mute = true;
    }
    public void EnableSound()
    {
        PlayerPrefs.SetInt("Sound", 1);
        AudioListener.pause = false;
    }
    public void EnableMusic()
    {
        PlayerPrefs.SetInt("Music", 1);
        AudioManager.instance.sounds[0].source.mute = false;
        AudioManager.instance.sounds[4].source.mute = false;
    }
}