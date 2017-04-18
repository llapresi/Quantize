using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour {

    public GameObject bulletPrefab;
    public Transform spawnLocation;
    public AudioSource audioSource;

    bool canFire;

    private void Start()
    {
        canFire = false;
    }

    public void OnBeat()
    {
        if(canFire == false)
        {
            canFire = true;
        }
    }

	// Update is called once per frame
	void Update () {
        if(canFire && Input.GetButton("Fire1"))
        {
            Object.Instantiate(bulletPrefab, spawnLocation.position, spawnLocation.rotation , null);
            audioSource.Play();
        }

        if (canFire)
            canFire = false;
	}
}
