using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {

    public static SFXManager instance = null;
    public SyncSoundEffect hatKill;
    public SyncSoundEffect kickKill;
    public SyncSoundEffect bassKill;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            Destroy(gameObject);
    }

    public void PlayKillSound(GameManager.HealthCategory type)
    {
        if(type == GameManager.HealthCategory.Hats)
        {
            hatKill.QueueSound();
        }

        if (type == GameManager.HealthCategory.Kick)
        {
            kickKill.QueueSound();
        }

        if (type == GameManager.HealthCategory.Bass)
        {
            bassKill.QueueSound();
        }
    }
}
