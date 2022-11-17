using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickWeapon : MonoBehaviour
{
    WeaponSwitcher weaponSwitcher;
    [SerializeField] int weaponPicked;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            weaponSwitcher = other.GetComponentInChildren<WeaponSwitcher>();
            weaponSwitcher.PickWeapon(weaponPicked);
            weaponSwitcher.ChangeWeapon(weaponPicked);
            Destroy(gameObject);
        }
    }

}
