using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Initial test of beat synchonization, using only time
/// </summary>
public class BeatRepeater : MonoBehaviour {

    public float bpm;
    public float division = 1;
    public int frequency = 44100;
    public int sampleOffset = 0;
    double nextBeat;
    double samplesPerBeat;
    double startBeatTime;
    double lastBeatTime;
    double CurrentTime;

    public UnityEvent OnBeat;

    float previousSample;

    public double NextBeat
    {
        get
        {
            return nextBeat;
        }
    }

    // Use this for initialization
    void Start () {

        samplesPerBeat = frequency / (bpm / 60) / division;
        nextBeat = startBeatTime = ((AudioSettings.dspTime - AudioManager.instance.DspStartTime) * frequency);
    }

    private void OnEnable()
    {
        nextBeat = double.MaxValue;
    }

    // Update is called once per frame
    void Update () {
        CurrentTime = ((AudioSettings.dspTime - AudioManager.instance.DspStartTime) * frequency);

        if (CurrentTime >= AudioManager.instance.DspStartTime)
        {

            if (CurrentTime >= nextBeat)
            {
                OnBeat.Invoke();

            }

            CalculateBeat();
        }
    }

    void CalculateBeat()
    {
        double newNextBeat = startBeatTime - sampleOffset;

        while(newNextBeat < CurrentTime)
        {
            newNextBeat += samplesPerBeat;
        }
        nextBeat = newNextBeat;
    }
}
