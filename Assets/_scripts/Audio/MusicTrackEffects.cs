using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicTrackEffects : MonoBehaviour {

    public GameManager.HealthCategory channel;
    public ShootScript weapon;
    float channelHealth;
    public string effectToToggle;
    public string channelVolumeName;
    public int effectThreshold;
    public int muteThreshold;

    float currentEffectValue;
    float currentVolume;

    float targetEffectValue;
    float targetVolume;

    public float rate;
	
	// Update is called once per frame
	void Update () {
        channelHealth = GameManager.instance.GetHealth(channel);
        AudioManager.instance.masterMixer.GetFloat(effectToToggle, out currentEffectValue);
        AudioManager.instance.masterMixer.GetFloat(channelVolumeName, out currentVolume);

        if(channelHealth > effectThreshold)
        {
            targetEffectValue = -90.0f;
            targetVolume = 0.0f;

        }
        else if (channelHealth > muteThreshold || weapon.isFiring)
        {
            targetEffectValue = 0.0f;
            targetVolume = -90.0f;
        }
        else
        {
            targetEffectValue = -90.0f;
            targetVolume = -90.0f;
        }

        AudioManager.instance.masterMixer.SetFloat(effectToToggle, Mathf.Lerp(currentEffectValue, targetEffectValue, Time.deltaTime * rate));
        AudioManager.instance.masterMixer.SetFloat(channelVolumeName, Mathf.Lerp(currentVolume, targetVolume, Time.deltaTime * rate));

    }
}
