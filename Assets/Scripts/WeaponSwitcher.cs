using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] int currentWeapon = 0;
    [SerializeField] public int[] weaponsActive = { 1, 0, 0 }; //not sure why didnt work
    [SerializeField] AudioClip pickUpSound;                    //with bool type
    void Start()                                               
    {
        //weaponsActive[0] = true;
        SetWeaponActive();
    }
    void Update()
    {
        int previousWeapon = currentWeapon;

        ProcessKeyInput();
        ProcessScrollWheel();
        if (previousWeapon != currentWeapon)
        {
            //Debug.Log(currentWeapon);
            if (weaponsActive[currentWeapon]==1)
            {
                SetWeaponActive();
            }
        }
    }


    void ProcessKeyInput()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && weaponsActive[0]==1)
        {
            currentWeapon = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && weaponsActive[1] == 1)
        {
            currentWeapon = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && weaponsActive[2] == 1)
        {
            currentWeapon = 2;
        }
    }

    void ProcessScrollWheel()
    {
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (currentWeapon >= (transform.childCount -1))
            {
                currentWeapon = 0;
            }
            else currentWeapon++;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (currentWeapon <= 0)
            {
                currentWeapon = (transform.childCount - 1);
            }
            else currentWeapon--;
        }
    }

    void SetWeaponActive()
    {
        int weaponIndex = 0;

        foreach(Transform weapon in transform)
        {
            if (weaponIndex == currentWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else weapon.gameObject.SetActive(false);

            weaponIndex++;
        }
    }

    public void PickWeapon(int weaponPicked)
    {
        GetComponent<AudioSource>().PlayOneShot(pickUpSound);
        weaponsActive[weaponPicked] = 1;
    }

    public void ChangeWeapon(int weaponChange)
    {
        currentWeapon = weaponChange;
        SetWeaponActive();
    }
}
