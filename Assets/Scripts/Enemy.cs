using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// This script defines 'Enemy's' health and behavior. 
/// </summary>
public class Enemy : MonoBehaviour
{
    int score = 50;
    private ScoreManager _score;
    // enemy health and projectile object
    public int health;
    public GameObject Projectile;
    // enemy VFX
    public GameObject destructionVFX;
    public GameObject hitEffect;
    // probability of enemy shooting 
    private float shotChance = 100f;
    private float shotTimeMin = 0, shotTimeMax = 0.5f;
    private void Start()
    {
        Invoke("ActivateShooting", Random.Range(shotTimeMin, shotTimeMax));
        _score = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreManager>();
    }

    //coroutine making a shot
    void ActivateShooting()
    {
        if (Random.value < (float)shotChance / 100)
        {
            Instantiate(Projectile, gameObject.transform.position, Quaternion.identity);
        }
    }
    // detect if enemy was damaged
    public void GetDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destruction();
            AudioManager.instance.PlaySound("ShipDies");
            _score.SetScore(score);
        }
        else
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity, transform);
        }
    }
    // if enemy collides with player destroy player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Projectile.GetComponent<Projectile>() != null)
            {
                Player.instance.GetDamage(Projectile.GetComponent<Projectile>().damage);
            }
            else
            {
                Player.instance.GetDamage(1);
            }
        }
    }
    // destroy enemy game obj
    void Destruction()
    {
        Instantiate(destructionVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }
}
