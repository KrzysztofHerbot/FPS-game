using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPSCamera;
    [SerializeField] float range = 1000f;
    [SerializeField] int weaponDamage = 20;
    [SerializeField] float timeBetweenShots = 0.1f;
    [SerializeField] float timeForReload = 1f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] AudioClip emptyClip;
    [SerializeField] AudioClip weaponClip;
    [SerializeField] AudioClip reloadClip1;
    [SerializeField] AudioClip reloadClip2;
    [SerializeField] GameObject worldHit;
    [SerializeField] GameObject enemyHit;
    [SerializeField] GameObject enemies;
    [SerializeField] TextMeshProUGUI ammoNumber;
    [SerializeField] AmmoType ammoType;
    Animator animatorAK;
    AudioSource ac;
    Ammo ammo;

    bool canShoot = true;
    bool canReload = true;


    private void Start()
    {
        ac = GetComponent<AudioSource>();
        ammo = GetComponentInParent<Ammo>();
        animatorAK = GetComponent<Animator>();
        //muzzleFlash.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        canShoot = true;
        canReload = true;
        
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && ammo.GetAmmoSlot(ammoType).CurrentAmmoInMagazine > 0 && canShoot == true)
        {
            StartCoroutine(Shoot());
        }
        else if(Input.GetButtonDown("Fire1") && ammo.GetAmmoSlot(ammoType).CurrentAmmoInMagazine == 0 && canReload == true)
        {
            StartCoroutine(Reload());
        }
            

        if (Input.GetKeyDown(KeyCode.R) && ammo.GetAmmoSlot(ammoType).CurrentAmmoInMagazine < ammo.GetAmmoSlot(ammoType).MaxAmmoInMagazine && canReload == true)
        {
            StartCoroutine(Reload());
        }

        ammoNumber.text = ammo.GetAmmoSlot(ammoType).CurrentAmmoInMagazine.ToString() + "/" + ammo.GetAmmoSlot(ammoType).CurrentAmmoInBackpack.ToString();

    }

    IEnumerator Shoot()
    {
        canShoot = false;
        PlayMuzzleflash();
        PlayWeaponSound();
        ProcessRaycast();
        ProcessAnimation();
        ProcessAmmo();
        EnemyNear();

        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    IEnumerator Reload()
    {
        canReload = false;
        if (ammo.GetAmmoSlot(ammoType).CurrentAmmoInBackpack > 0 && ammo.GetAmmoSlot(ammoType).CurrentAmmoInMagazine != ammo.GetAmmoSlot(ammoType).MaxAmmoInMagazine)
        {
            ac.PlayOneShot(reloadClip1);
            animatorAK.SetTrigger("Reload");
        }
        else ac.PlayOneShot(emptyClip);
        yield return new WaitForSeconds(timeForReload);
        canReload = true;

    }

    void ChangeAmmountInMagazine()
    {
        ac.PlayOneShot(reloadClip2);
        ammo.Reload(ammoType);
    }

    private void PlayMuzzleflash()
    {
        
        //muzzleFlash.gameObject.SetActive(true);
        muzzleFlash.Play();
    }
    private void PlayWeaponSound()
    {
        ac.PlayOneShot(weaponClip);
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPSCamera.transform.position, FPSCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            Debug.Log("You hit: " + hit.transform.name);
            EnemyHealth targetMain = hit.transform.GetComponent<EnemyHealth>();
            HitboxEnemy targetBodypart =  hit.transform.GetComponent<HitboxEnemy>();
            if (targetMain != null)
            {
                targetMain.TakeDamage(weaponDamage);
                //call a method on enemyhealth
            }
            else if (targetBodypart != null)
            {
                targetBodypart.TakeDamageBodypart(weaponDamage);
                //call a method on HitboxEnemy
            }
        }
        else return;
    }

    void EnemyNear()
    {
        Debug.Log("enemyNear");
        enemies.BroadcastMessage("OnWeaponShot", transform.position,SendMessageOptions.DontRequireReceiver);
    }

    void ProcessAnimation()
    {
        animatorAK.SetTrigger("Shoot");
    }
    void ProcessAmmo()
    {
        ammo.ReduceAmmoInMagazine(1,ammoType);
    }

    void CreateHitImpact(RaycastHit hit)
    {
        //EnemyAI scriptAI = hit.transform.GetComponent<EnemyAI>();
        if(hit.transform.GetComponent<HitboxEnemy>() !=null)
        {
            Instantiate(enemyHit, hit.point, Quaternion.LookRotation(hit.normal));
        }
        else Instantiate(worldHit, hit.point, Quaternion.LookRotation(hit.normal));
    }

}
