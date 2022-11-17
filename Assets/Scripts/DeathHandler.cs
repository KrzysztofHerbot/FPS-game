using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverScreen;
    [SerializeField] Canvas hpScreen;

    private void Start()
    {
        gameOverScreen.enabled = false;
        hpScreen.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void HandleDeath()
    {
       //disables script components for moving camera and shooting weapon
        GetComponent<RigidbodyFirstPersonController>().enabled = false;
        GetComponent<FirstPersonController>().enabled = false;
        if(GetComponentInChildren<Weapon>() != null)
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

        //Shows game over canva
        gameOverScreen.enabled = true;
        hpScreen.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

}
