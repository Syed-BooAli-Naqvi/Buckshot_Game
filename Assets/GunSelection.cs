using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSelection : MonoBehaviour
{
    public bool test;
    public int peekaboo;
    public PlayerWeapons playerWeapons;
    public WeaponPickup[] weaponPickup;
    private void OnValidate()
    {
        if (test)
        {
            test = false;
            playerWeapons.DropWeapon(playerWeapons.currentWeapon);
            weaponPickup[peekaboo].PickUpItem();
        }
    }
}
