using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelect : MonoBehaviour {

    public GameObject[] weapons;
    bool canSelect = true;

    GameManager.HealthCategory currentWeapon = GameManager.HealthCategory.Kick;

    public GameManager.HealthCategory CurrentWeapon
    {
        get
        {
            return currentWeapon;
        }
    }

    // Update is called once per frame
    void Update () {
        if (canSelect)
        {
            if (Input.GetButtonDown("Weapon 1") || Input.GetAxisRaw("Weapon 1") > 0.3)
            {
                for (int i = 0; i < weapons.Length; i++)
                {
                    weapons[i].SetActive(false);
                    weapons[0].SetActive(true);
                    currentWeapon = GameManager.HealthCategory.Kick;
                }
            }
            if (Input.GetButtonDown("Weapon 2"))
            {
                for (int i = 0; i < weapons.Length; i++)
                {
                    weapons[i].SetActive(false);
                    weapons[1].SetActive(true);
                    currentWeapon = GameManager.HealthCategory.Bass;
                }
            }
            if (Input.GetButtonDown("Weapon 3") || Input.GetAxisRaw("Weapon 3") > 0.3)
            {
                for (int i = 0; i < weapons.Length; i++)
                {
                    weapons[i].SetActive(false);
                    weapons[2].SetActive(true);
                    currentWeapon = GameManager.HealthCategory.Hats;
                }
            }
        }
    }

    public void SetAllActive(bool toSet)
    {
        canSelect = toSet;
        if(canSelect == false)
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].SetActive(false);
                weapons[0].SetActive(true);
            }
        }
    }
}
