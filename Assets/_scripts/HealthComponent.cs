using UnityEngine;
using UnityEngine.Events;
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
    bool isStunned = false;

    public UnityEvent onStun;

    // Method to change the amount of health the health component has
    // Pass negative values to deal damage, pass positive values to give health
    public void ChangeHealth(int changeAmt)
    {
        healthCount += changeAmt;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(healthCount <= 0 && !isPlayer)
        {
            isStunned = true;
            onStun.Invoke();
        }
	}
}