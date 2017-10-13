using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePool : MonoBehaviour
{

    AudioSource[] audioSources;
    bool[] isScheduled;
    public int sourceCount;


    // Use this for initialization
    void Start()
    {
        audioSources = new AudioSource[sourceCount];
        isScheduled = new bool[sourceCount];
        GameObject child = new GameObject("Player");

        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i] = child.AddComponent<AudioSource>();
            isScheduled[i] = false;
        }
    }

    // Update is called once per frame
    public void PlaySound(AudioClip sound, double timeOnTimeline)
    {
        bool activesound = false;
        int iterator = 0;
        while (activesound == false)
        {
            if (isScheduled[iterator] == false)
            {
                activesound = true;
                isScheduled[iterator] = true;
                audioSources[iterator].clip = sound;
                audioSources[iterator].PlayScheduled(timeOnTimeline);
                audioSources[iterator].SetScheduledEndTime(timeOnTimeline + sound.length);
                StartCoroutine(RemovedScheduled((float)((timeOnTimeline + sound.length) - AudioSettings.dspTime), iterator));
            }
            else
            {
                iterator += 1;
            }
        }
    }

    private void Update()
    {
        int active = 0;
        for (int i = 0; i < isScheduled.Length; i++)
        {
            if (isScheduled[i] == true)
                active += 1;
        }
    }

    IEnumerator RemovedScheduled(float delayTime, int iterator)
    {
        yield return new WaitForSeconds(delayTime);
        // Now do your thing here
        isScheduled[iterator] = false;
    }
}