using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelect : MonoBehaviour {

    public GameObject[] weapons;
    bool canSelect = true;
	
	// Update is called once per frame
	void Update () {
        if (canSelect)
        {
            if (Input.GetKeyDown("1"))
            {
                for (int i = 0; i < weapons.Length; i++)
                {
                    weapons[i].SetActive(false);
                    weapons[0].SetActive(true);
                }
            }
            if (Input.GetKeyDown("2"))
            {
                for (int i = 0; i < weapons.Length; i++)
                {
                    weapons[i].SetActive(false);
                    weapons[1].SetActive(true);
                }
            }
            if (Input.GetKeyDown("3"))
            {
                for (int i = 0; i < weapons.Length; i++)
                {
                    weapons[i].SetActive(false);
                    weapons[2].SetActive(true);
                }
            }
        }
    }

    public void SetActive(bool toSet)
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
