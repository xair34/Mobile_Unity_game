using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject destructionFX;
    public GameObject gameOverPanel;
    public Text gameOverScore;
    public Text scoreBoard;
    public static Player instance; 
    // check if there is only one player instance
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // check if player is hit with enemy projectile
    public void GetDamage(int damage)   
    {
        Destruction();
        AudioManager.instance.PlaySound("ShipDies");
        AudioManager.instance.PlaySound("PlayerDiedTheme");
        AudioManager.instance.StopPlayingSound("BattleTheme");
        gameOverScore.text = "Score: " + scoreBoard.text;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        
    }    
    // destroy player and play VFX explosion
    void Destruction()
    {
        Instantiate(destructionFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
















