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
    double secondsPerBeat;
    double startBeatTime;
    double lastBeatTime;
    double CurrentTime;
    public bool renderDebug = false;

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

        secondsPerBeat = (60 / bpm) / division;
        nextBeat = startBeatTime = (AudioManager.instance.DspStartTime);
    }

    // Fixes bug where beats "play catch up" after a repeater has been deactivated and then reactivated (ex. the weapons)
    private void OnEnable()
    {
        nextBeat = double.MinValue;
    }

    // Update is called once per frame
    void Update () {
        CurrentTime = (AudioSettings.dspTime);

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
        double newNextBeat = startBeatTime;

        while(newNextBeat < CurrentTime)
        {
            newNextBeat += secondsPerBeat;
        }
        nextBeat = newNextBeat;
    }
}
