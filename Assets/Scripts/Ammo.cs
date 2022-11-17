using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] AmmoSlot[] ammoSlots;
    [System.Serializable]
    public class AmmoSlot
    {
        public AmmoType ammoType;
        public int currentAmmoInMagazine;
        public int CurrentAmmoInMagazine { get { return currentAmmoInMagazine; } }
        public int maxAmmoInMagazine;
        public int MaxAmmoInMagazine { get { return maxAmmoInMagazine; } }
        public int currentAmmoInBackpack;
        public int CurrentAmmoInBackpack { get { return currentAmmoInBackpack; } }
        public int maxAmmoInBackpack;
        public int MaxAmmoInBackpack { get { return maxAmmoInBackpack; } }

    }

   /* [SerializeField] int currentAmmoInMagazine=20;
    public int CurrentAmmoInMagazine { get { return currentAmmoInMagazine; } }

    [SerializeField] int maxAmmoInMagazine=20;
    public int MaxAmmoInMagazine { get { return maxAmmoInMagazine; } }

    [SerializeField] int currentAmmoInBackpack = 40;
    public int CurrentAmmoInBackpack { get { return currentAmmoInBackpack; } }

    [SerializeField] int maxAmmoInBackpack = 120;
    public int MaxAmmoInBackpack { get { return maxAmmoInBackpack; } }
   */

    public void Reload(AmmoType ammoType)
    {
        int ammountToReload = GetAmmoSlot(ammoType).maxAmmoInMagazine - GetAmmoSlot(ammoType).currentAmmoInMagazine;

        if (ammountToReload >= GetAmmoSlot(ammoType).currentAmmoInBackpack)
        {
            AddAmmoInMagazine(GetAmmoSlot(ammoType).currentAmmoInBackpack,ammoType);
            ReduceAmmoInBackpack(GetAmmoSlot(ammoType).currentAmmoInBackpack,ammoType);
        }
        else
        {
            AddAmmoInMagazine(ammountToReload,ammoType);
            ReduceAmmoInBackpack(ammountToReload,ammoType);
        }

    }

    public void AddAmmoInMagazine(int add, AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).currentAmmoInMagazine = GetAmmoSlot(ammoType).currentAmmoInMagazine + add;
    }
    public void ReduceAmmoInMagazine(int reduced, AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).currentAmmoInMagazine = GetAmmoSlot(ammoType).currentAmmoInMagazine - reduced;
    }

    public void AddAmmoInBackpack(int add, AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).currentAmmoInBackpack = GetAmmoSlot(ammoType).currentAmmoInBackpack + add;
    }
    public void ReduceAmmoInBackpack(int reduced, AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).currentAmmoInBackpack = GetAmmoSlot(ammoType).currentAmmoInBackpack - reduced;
    }

    public AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach (AmmoSlot slot in ammoSlots)
        {
            if (slot.ammoType == ammoType)
            {
                return slot;
            }
        }
        return null;
    }

}
