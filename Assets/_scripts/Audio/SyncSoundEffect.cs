using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncSoundEffect : MonoBehaviour {

    public AudioClip sound;
    public BeatRepeater repeater;
    public AudioSourcePool soundPool;

    // Update is called once per frame
    public void PlaySound () {
        soundPool.PlaySound(sound, repeater.NextBeat);
    }
}
