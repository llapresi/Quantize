﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    // Use this for initialization
    public void QuitGame()
    {
        Application.Quit();
    }

    public void StarGame()
    {
        SceneManager.LoadScene(1);
    }
}