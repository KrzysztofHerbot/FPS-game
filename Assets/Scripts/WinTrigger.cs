using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] Canvas winGameScreen;
    [SerializeField] Canvas playerInterfaceScreen;
    [SerializeField] AudioClip winSoundClip;
    Animator anim;
    bool isStart;
    GameObject heli;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
        isStart = true;
        playerInterfaceScreen.enabled = isStart;
        winGameScreen.enabled = false;
        GetComponent<CharacterController>().enabled = isStart;
        GetComponent<PlayerHealth>().enabled = isStart;
        GetComponent<DeathHandler>().enabled = isStart;
        GetComponent<WeaponZoom>().enabled = isStart;
        GetComponent<Flashlight>().enabled = isStart;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            isStart = false;
            EndGame();
        }
    }
    void EndGame()
    {
        GameObject.FindGameObjectWithTag("Heli").GetComponent<Animator>().SetTrigger("fly");
        HandleDeath();
        playerInterfaceScreen.enabled = isStart;
        GetComponent<CharacterController>().enabled = isStart;
        GetComponent<PlayerHealth>().enabled = isStart;
        GetComponent<DeathHandler>().enabled = isStart;
        GetComponent<WeaponZoom>().enabled = isStart;
        GetComponent<Flashlight>().enabled = isStart;
        anim.enabled = true;
        GetComponent<AudioSource>().PlayOneShot(winSoundClip);
        anim.SetTrigger("win");

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }

        Invoke("ShowEndScreen", 5f);
        
    }

    void ShowEndScreen()
    {
        winGameScreen.enabled = true;
        Time.timeScale = 0;
    }
    void HandleDeath()
    {
        //disables script components for moving camera and shooting weapon
        GetComponent<RigidbodyFirstPersonController>().enabled = false;
        GetComponent<FirstPersonController>().enabled = false;
        if (GetComponentInChildren<Weapon>() != null)
        {
            GetComponentInChildren<Weapon>().enabled = false;
        }
        if (GetComponentInChildren<BlankWeapon>() != null)
        {
            GetComponentInChildren<BlankWeapon>().enabled = false;
        }

        //disables weapon mesh on screen
        MeshRenderer[] weaponMesh;
        weaponMesh = GetComponentsInChildren<MeshRenderer>();//.enabled = false;
        foreach (MeshRenderer mesh in weaponMesh)
        {
            mesh.enabled = false;
        }


        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1;

    }
}
