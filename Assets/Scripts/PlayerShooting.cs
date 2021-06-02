using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//player's guns
[System.Serializable]
public class Guns
{
    public GameObject rightGun, leftGun, centralGun;
    [HideInInspector] public ParticleSystem leftGunVFX, rightGunVFX, centralGunVFX;
}

public class PlayerShooting : MonoBehaviour
{
    float timeBeforeGameStarts = 2f;
    // player gun settings - fire rate, projectile image, fire rate cooldown, weapon power
    public float fireRate;
    public GameObject projectileObject;
    [HideInInspector] public float nextFire;
    public int weaponPower = 1;
    public Guns guns;
    [HideInInspector] public int maxweaponPower = 4;
    public static PlayerShooting instance;

    // check if there is only 1 playershooting instance
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
    private void Start()
    {
        //receiving shooting visual effects components
        guns.leftGunVFX = guns.leftGun.GetComponent<ParticleSystem>();
        guns.rightGunVFX = guns.rightGun.GetComponent<ParticleSystem>();
        guns.centralGunVFX = guns.centralGun.GetComponent<ParticleSystem>();
        StartCoroutine(SmallPauseBforestart());
    }
    IEnumerator SmallPauseBforestart()
    {
        yield return new WaitForSeconds(timeBeforeGameStarts);
        while (timeBeforeGameStarts > 0)
        {
            timeBeforeGameStarts -= 1;
        }
        Debug.Log(timeBeforeGameStarts);
    }
    private void Update()
    {
        if (timeBeforeGameStarts <= 0)
        {
            if (Time.time > nextFire)
            {
                MakeAShot();
                AudioManager.instance.PlaySound("PlayerShoots");
                nextFire = Time.time + fireRate / 1.75f;
            }
        }

        // method to make player shoot
        void MakeAShot()
        {
            // switch fire methods based on weapon power
            switch (weaponPower)
            {
                case 1:
                    CreateLazerShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
                    guns.centralGunVFX.Play();
                    break;
                case 2:
                    CreateLazerShot(projectileObject, guns.rightGun.transform.position, Vector3.zero);
                    guns.leftGunVFX.Play();
                    CreateLazerShot(projectileObject, guns.leftGun.transform.position, Vector3.zero);
                    guns.rightGunVFX.Play();
                    break;
                case 3:
                    CreateLazerShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
                    CreateLazerShot(projectileObject, guns.rightGun.transform.position, new Vector3(0, 0, -5));
                    guns.leftGunVFX.Play();
                    CreateLazerShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 5));
                    guns.rightGunVFX.Play();
                    break;
                case 4:
                    CreateLazerShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
                    CreateLazerShot(projectileObject, guns.rightGun.transform.position, new Vector3(0, 0, -5));
                    guns.leftGunVFX.Play();
                    CreateLazerShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 5));
                    guns.rightGunVFX.Play();
                    CreateLazerShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 15));
                    CreateLazerShot(projectileObject, guns.rightGun.transform.position, new Vector3(0, 0, -15));
                    break;
            }
        }
        // create the player's projectile object on the position on the gun based on weapon power
        void CreateLazerShot(GameObject lazer, Vector3 pos, Vector3 rot)
        {
            Instantiate(lazer, pos, Quaternion.Euler(rot));
        }
    }
}
