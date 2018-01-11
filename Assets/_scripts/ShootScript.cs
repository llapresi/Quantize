using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour {

    public GameObject bulletPrefab;
    public Transform spawnLocation;
    GameObject[] pooledBullets;
    public int poolLength;

    bool canFire = false;
    public bool isFiring = false;

    private void Start()
    {
        // Create bullet object pool
        pooledBullets = new GameObject[poolLength];

        for (int i = 0; i < pooledBullets.Length; i++)
        {
            GameObject obj = (GameObject)Instantiate(bulletPrefab);
            obj.SetActive(false);
            pooledBullets[i] = obj;
        }
    }

    void ResetBullets()
    {
        for (int i = 0; i < pooledBullets.Length; i++)
        {
            pooledBullets[i].SetActive(false);
        }
    }

    private void OnEnable()
    {
        canFire = false;
        isFiring = false;
    }

    private void OnDisable()
    {
        canFire = false;
        isFiring = false;
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
        Vector2 rightStick = Vector2.zero;
        rightStick.x = Input.GetAxisRaw("Right Joystick X");
        rightStick.y = Input.GetAxisRaw("Right Joystick Y");

        if (Input.GetButton("Fire1") || rightStick.magnitude > 0.3f)
            isFiring = true;
        else
            isFiring = false;

        if(canFire && (Input.GetButton("Fire1") || rightStick.magnitude > 0.3f))
        {
            bool activebullet = false;
            int iterator = 0;
            while(activebullet == false)
            {
                if(pooledBullets[iterator].activeInHierarchy == false)
                {
                    pooledBullets[iterator].SetActive(true);
                    pooledBullets[iterator].GetComponent<BulletMovement>().Fire(spawnLocation);
                    activebullet = true;
                }
                else
                {
                    iterator += 1;
                }
            }
        }
        canFire = false;
	}
}
