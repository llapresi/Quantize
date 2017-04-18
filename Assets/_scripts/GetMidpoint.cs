using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMidpoint : MonoBehaviour {

    public Transform point1;
    public Transform point2;

    [Range(0.0f, 1.0f)]
    public float xLerp;

    [Range(0.0f, 1.0f)]
    public float yLerp;
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(Mathf.Lerp(point1.position.x, point2.position.x, xLerp), Mathf.Lerp(point1.position.y, point2.position.y, yLerp), 0f);

    }
}
