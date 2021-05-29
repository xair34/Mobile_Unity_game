using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Projectile : MonoBehaviour {

    // projectile settings
    public int damage;
    public bool enemyBullet;
    public bool destroyedByCollision;
    // check if projectile collision is player or enemy and destroy the obj
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyBullet && collision.tag == "Player")
        {
            Player.instance.GetDamage(damage); 
            if (destroyedByCollision)
            {
                Destruction();
            }
        }
        else if (!enemyBullet && collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().GetDamage(damage);
            if (destroyedByCollision)
            {
                Destruction();
            }
        }
    }
    void Destruction() 
    {
        Destroy(gameObject);
    }
}


