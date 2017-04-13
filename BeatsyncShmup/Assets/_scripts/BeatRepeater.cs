using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Initial test of beat synchonization, using only time
/// </summary>
public class BeatRepeater : MonoBehaviour {

    public float bpm;
    public AudioClip song;
    AudioSource source;
    float nextBeat;
    float samplesPerBeat;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        source.clip = song;
        source.Play();

        samplesPerBeat = song.frequency / (bpm / 60);
        CalculateBeat();
    }
	
	// Update is called once per frame
	void Update () {
		if(source.timeSamples >= nextBeat)
        {
            //Debug.Log("beat");
            //CalculateBeat();
        }

        Debug.Log("dspTime: " + (float)(AudioSettings.dspTime));
        Debug.Log("TS: " + source.timeSamples);
    }

    void CalculateBeat()
    {
        nextBeat = source.timeSamples + samplesPerBeat;
    }
}
