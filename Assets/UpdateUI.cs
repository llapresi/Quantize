using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateUI : MonoBehaviour {

    public RectTransform kickHealthUI;
    public RectTransform bassHealthUI;
    public RectTransform hatsHealthUI;
    public UnityEngine.UI.Text scoreUI;
    public UnityEngine.UI.Text livesUI;
    public UnityEngine.UI.Text endUI;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        kickHealthUI.localScale = new Vector3(GameManager.instance.GetHealth(GameManager.HealthCategory.Kick) / 100, 1);
        bassHealthUI.localScale = new Vector3(GameManager.instance.GetHealth(GameManager.HealthCategory.Bass) / 100, 1);
        hatsHealthUI.localScale = new Vector3(GameManager.instance.GetHealth(GameManager.HealthCategory.Hats) / 100, 1);
        livesUI.text = "lives: " + GameManager.instance.PlayerLives;
    }

    public void UpdateScore(int score)
    {
        scoreUI.text = "score: " + score;
    }

    public void ShowFinalScreen()
    {
        endUI.text = "Game over\nFinal Score: " + GameManager.instance.Score;
        endUI.enabled = true;
    }
}
