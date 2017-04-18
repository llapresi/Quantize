using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitLifetime : MonoBehaviour {

    public float lifetimeSeconds;
    private float initializationTime;

    private void Start()
    {
        initializationTime = Time.timeSinceLevelLoad;
    } 

    // Update is called once per frame
    void Update () {
        float timeSinceInitialization = Time.timeSinceLevelLoad - initializationTime;
        
        if(timeSinceInitialization > lifetimeSeconds)
        {
            Destroy(this.gameObject);
        }
	}
}
