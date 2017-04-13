using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Initial test of beat synchonization, using only time
/// </summary>
public class BeatRepeater : MonoBehaviour {

    public float bpm;
    public AudioClip song;
    public AudioSource source;
    float nextBeat;
    float samplesPerBeat;
    double dspTimeStart;

    public UnityEvent OnBeat;

    float previousSample;

    // Use this for initialization
    void Start () {
        source.clip = song;
        source.Play();


        samplesPerBeat = song.frequency / (bpm / 60);
        dspTimeStart = AudioSettings.dspTime;
        nextBeat = 0f;
        CalculateBeat();
        Debug.Log(nextBeat);
    }
	
	// Update is called once per frame
	void Update () {
        //float CurrentTime = source.timeSamples;
        float CurrentTime = (float)(AudioSettings.dspTime - dspTimeStart ) * source.clip.frequency;


        if ((CurrentTime) >= nextBeat)
        {
            OnBeat.Invoke();
            CalculateBeat();
        }
    }

    void CalculateBeat()
    {
        nextBeat = nextBeat + samplesPerBeat;
    }
}
