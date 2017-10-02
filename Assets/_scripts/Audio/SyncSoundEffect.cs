using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncSoundEffect : MonoBehaviour {

    int queue;
    AudioSource source;
    BeatRepeater repeater;
    public bool alwaysQueue;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        repeater = GetComponent<BeatRepeater>();
    }

    // Update is called once per frame
    public void PlaySound () {
		if (queue > 0 || alwaysQueue) {
            source.PlayScheduled(repeater.NextBeat / 44100);
            queue -= 1;
        }
	}

    public void QueueSound()
    {
        queue += 1;
    }
}
