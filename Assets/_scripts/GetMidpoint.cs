using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMidpoint : MonoBehaviour {

    public Transform Track;

    [Range(0.0f, 1.0f)]
    public float xLerp;

    [Range(0.0f, 1.0f)]
    public float yLerp;

    public float TrackSpeed;
	
	// Update is called once per frame
	void Update () {
        Vector3 newPos = new Vector3(Mathf.Lerp(0, Track.position.x, xLerp), Mathf.Lerp(0, Track.position.y, yLerp), this.transform.position.z);
        this.transform.position = Vector3.MoveTowards(this.transform.position, newPos, Time.deltaTime * TrackSpeed);

    }
}
