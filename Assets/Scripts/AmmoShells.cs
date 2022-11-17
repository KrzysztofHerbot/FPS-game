using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoShells : MonoBehaviour
{
    [SerializeField] int ammoAmount = 5;
    [SerializeField] AmmoType ammoType;
    [SerializeField] GameObject pickUpSoundObject;
    Ammo ammo;
    bool isPicked = false;

    private void Start()
    {
        ammo = FindObjectOfType<Ammo>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            int ammoInBackpack = ammo.GetAmmoSlot(ammoType).currentAmmoInBackpack;
            int maxAmmoInBackpack = ammo.GetAmmoSlot(ammoType).maxAmmoInBackpack;

            if (ammoInBackpack == maxAmmoInBackpack) return;

            if (ammoInBackpack + ammoAmount >= maxAmmoInBackpack && isPicked != true)
            {
                isPicked = true;
                ammo.AddAmmoInBackpack(maxAmmoInBackpack - ammoAmount, ammoType);
                Instantiate(pickUpSoundObject);
                Destroy(gameObject);
            }
            else if(ammoInBackpack + ammoAmount < maxAmmoInBackpack && isPicked != true)
            {
                isPicked = true;
                ammo.AddAmmoInBackpack(ammoAmount, ammoType);
                Instantiate(pickUpSoundObject);
                Destroy(gameObject);
            }
            
        }
    }
}
