using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BlankWeapon : MonoBehaviour
{
    [SerializeField] Camera FPSCamera;
    [SerializeField] float range = 4f;
    [SerializeField] int weaponDamage = 20;
    [SerializeField] float timeBetweenShots = 0.1f;
    [SerializeField] AudioClip weaponClip;
    [SerializeField] AudioClip weaponClipMiss;
    [SerializeField] GameObject worldHit;
    [SerializeField] GameObject enemyHit;
    [SerializeField] TextMeshProUGUI ammoNumber;
    [SerializeField] AmmoType ammoType;
    Animator animatorAK;
    AudioSource ac;

    bool canShoot = true;
    bool canReload = true;


    private void Start()
    {
        ac = GetComponent<AudioSource>();
        animatorAK = GetComponent<Animator>();
        //muzzleFlash.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        canShoot = true;
        canReload = true;
        ammoNumber.text = " ";
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canShoot == true)
        {
            StartCoroutine(Shoot());
        }

    }

    IEnumerator Shoot()
    {
        canShoot = false;
        //PlayWeaponSound();
        ProcessRaycast();
        ProcessAnimation();

        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
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
            PlayWeaponSound();
            Debug.Log("You hit: " + hit.transform.name);
            EnemyHealth targetMain = hit.transform.GetComponent<EnemyHealth>();
            HitboxEnemy targetBodypart = hit.transform.GetComponent<HitboxEnemy>();
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
        else
        {
            ac.PlayOneShot(weaponClipMiss);
            return;
        }
    }

    void ProcessAnimation()
    {
        animatorAK.SetTrigger("Shoot");
    }

    void CreateHitImpact(RaycastHit hit)
    {
        //EnemyAI scriptAI = hit.transform.GetComponent<EnemyAI>();
        if (hit.transform.GetComponent<HitboxEnemy>() != null)
        {
            Instantiate(enemyHit, hit.point, Quaternion.LookRotation(hit.normal));
        }
        else Instantiate(worldHit, hit.point, Quaternion.LookRotation(hit.normal));
    }

}

