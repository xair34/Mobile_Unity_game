using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    // score tracking
    int score = 50;
    private ScoreManager _score;
    // enemy health and projectile object
    public int health;
    public GameObject Projectile;
    // enemy VFX
    public GameObject destructionVFX;
    public GameObject hitEffect;
    private float enemyCooldownTimer = 0.5f;
    private void Start()
    {
        _score = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreManager>();
    }
    private void Update()
    {
        EnemyAttack();
    }
    void EnemyAttack()
    {
        enemyCooldownTimer -= Time.deltaTime;
        if (enemyCooldownTimer <= 0)
        {
            Instantiate(Projectile, gameObject.transform.position, Quaternion.identity);
            enemyCooldownTimer = 2f;
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
            Player.instance.GetDamage(1);
        }
    }
    // destroy enemy game obj
    void Destruction()
    {
        Instantiate(destructionVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
