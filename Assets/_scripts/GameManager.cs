using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public enum HealthCategory
    {
       Kick,
       Bass,
       Hats,
       Chords
    }

    [SerializeField]
    private float kickHealth, bassHealth, hatsHealth, decayAmount = 0.5f, healthCap;

    GameObject playerRef;
    public UnityEngine.PostProcessing.PostProcessingBehaviour cameraPP;

    public int PlayerLives = 3;
    int score = 0;

    public int Score
    {
        get
        {
            return score;
        }
    }

    UpdateUI gameUI;

    public GameObject PlayerRef
    {
        get
        {
            return playerRef;
        }
    }

    public void Start()
    {
        if(QualitySettings.GetQualityLevel() == 1)
        {
            cameraPP.enabled = true;
        }
        else
        {
            cameraPP.enabled = false;
        }
    }

    public void ChangeHealth(HealthCategory toChange, float amount)
    {
        if (toChange == HealthCategory.Kick)
            kickHealth += amount;
        if (toChange == HealthCategory.Bass)
            bassHealth += amount;
        if (toChange == HealthCategory.Hats)
            hatsHealth += amount;
    }

    public float GetHealth(HealthCategory toGet)
    {
        if (toGet == HealthCategory.Kick)
            return kickHealth;
        if (toGet == HealthCategory.Bass)
            return bassHealth;
        if (toGet == HealthCategory.Hats)
            return hatsHealth;

        return 0.0f;
    }

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            Destroy(gameObject);

        playerRef = GameObject.FindGameObjectWithTag("Player");
        gameUI = GameObject.Find("Canvas").GetComponent<UpdateUI>();
    }

    // Update is called once per frame
    void Update () {
        // Remove decay amount from each of the health pools
        kickHealth -= decayAmount * Time.deltaTime;
        bassHealth -= decayAmount * Time.deltaTime;
        hatsHealth -= decayAmount * Time.deltaTime;

        // Prevent health below 0
        if (kickHealth < 0)
            kickHealth = 0;
        if (bassHealth < 0)
            bassHealth = 0;
        if (hatsHealth < 0)
            hatsHealth = 0;

        // Prevent health above health-cap
        if (kickHealth > healthCap)
            kickHealth = healthCap;
        if (bassHealth > healthCap)
            bassHealth = healthCap;
        if (hatsHealth > healthCap)
            hatsHealth = healthCap;

    }

    public void AddScore(int amount)
    {
        if (kickHealth > 0 && hatsHealth > 0 && bassHealth > 0)
        {
            score += amount;
            gameUI.UpdateScore(score);
        }
    }

    public void EndGame()
    {
        kickHealth = 15;
        bassHealth = 15;
        hatsHealth = 15;
        gameUI.ShowFinalScreen();
        StartCoroutine(ReturnToMenu());
    }

    IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }
}
