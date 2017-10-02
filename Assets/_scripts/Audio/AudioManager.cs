using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    //Setup for syncing audio
    //public AudioSource master;
    public AudioSource[] sources;
    private IEnumerator coroutine;
    public AudioMixer masterMixer;
    private double dspStartTime;

    public static AudioManager instance = null;

    private float die = 0;

    public double DspStartTime
    {
        get
        {
            return dspStartTime;
        }
    }

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            Destroy(gameObject);

        dspStartTime = AudioSettings.dspTime + 3;
        StartCoroutine(StartCountDown());
    }

    IEnumerator StartCountDown()
    {
        UnityEngine.UI.Text uiLabel = GameObject.Find("GameOverUI").GetComponent<UnityEngine.UI.Text>();
        uiLabel.enabled = true;
        uiLabel.text = "3";
        yield return new WaitForSeconds(1);
        uiLabel.text = "2";
        yield return new WaitForSeconds(1);
        uiLabel.text = "1";
        yield return new WaitForSeconds(1);
        uiLabel.enabled = false;
    }


    /*private IEnumerator SyncSources()
    {
        while (true)
        {
            for (int i = 0; i < slaves.Length; i++)
            {
                slaves[i].timeSamples = master.timeSamples;
                yield return null;
            }
        }
    } */

    private void Start()
    {
        /*coroutine = SyncSources();
        StartCoroutine(coroutine);

        AudioManager.instance.masterMixer.SetFloat("bassVolume", -90.0f);
        AudioManager.instance.masterMixer.SetFloat("chordsVolume", -90.0f);
        AudioManager.instance.masterMixer.SetFloat("hatsVolume", -90.0f);
        AudioManager.instance.masterMixer.SetFloat("kickVolume", 0.0f); */

        for (int i = 0; i < sources.Length; i++)
        {
            sources[i].PlayScheduled(dspStartTime);
            Debug.Log(dspStartTime);
        }
    }

    void Update()
    {
        float dieLowPass = Mathf.Lerp(7000.0f, 0.0f, die);
        die -= 0.1f * Time.deltaTime;
        masterMixer.SetFloat("masterLowPass", dieLowPass);
        if (die > 0.5) {
            masterMixer.SetFloat("masterLPWet", 0.0f);

        }
        else
        {
            masterMixer.SetFloat("masterLPWet", -80.0f);
        }

    }

    public void Die()
    {
        die = 1;
    }
}
