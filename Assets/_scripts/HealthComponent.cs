using UnityEngine;
using System.Collections;

public class HealthComponent : MonoBehaviour {

    // Stores the number of health points, defaults to 100
    [SerializeField]
    private int healthCount = 100;

    [SerializeField]
    private bool isPlayer;

    [SerializeField]
    //private GameObject deathParticlePrefab;

    private bool hasDied = false;

    // Method to change the amount of health the health component has
    // Pass negative values to deal damage, pass positive values to give health
    public void ChangeHealth(int changeAmt)
    {
        healthCount += changeAmt;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(healthCount <= 0)
        {
            // Destroy gameobject with component on kill, REPLACE THIS WITH A BETTER SYSTEM USING
            // UnityEngine.Events LATER
            if (isPlayer && !hasDied)
            {
                hasDied = true;
                StartCoroutine(PlayerRespawn());
            }
            else if(!hasDied && !isPlayer)
            {
                EnemyComponent enemyC = gameObject.GetComponent<EnemyComponent>();
                if(enemyC != null)
                {
                    enemyC.OnKill();
                }
                //Instantiate(deathParticlePrefab, transform.position, Quaternion.Euler(0, 0, 0));
                Destroy(this.gameObject);
            }
        }
	}

    IEnumerator PlayerRespawn()
    {
        GameObject.FindGameObjectWithTag("CameraTarget").transform.localPosition = Vector3.zero;
        GetComponent<PlayerMovemet>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        // Get list of all child objects
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(false);
        }

        //Instantiate(deathParticlePrefab, transform.position, Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(2);
        Application.LoadLevel(Application.loadedLevel);
    }
}