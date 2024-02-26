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
        }
    }
    IEnumerator Start()
    {
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(3);
            peekaboo = Random.Range(0, 6);
            weaponPickup[peekaboo].PickUpItem();
        }
    }
    public void Select(int peekaboo)
    {
    }
}
