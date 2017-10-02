using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateWalls : MonoBehaviour {

    public Material material;
    public Color startColor;
    public Color endColor;
    private float currentTime;
    public float speed;

    public void AnimateBeat()
    {
        currentTime = 0;
    }

    void Update()
    {
        currentTime += speed * Time.deltaTime;
        material.color = Color.Lerp(startColor, endColor, currentTime);
    }
}