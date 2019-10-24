using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void PlaySinglePlayer()
    {
        Options.CoopMode = false;
        Options.CheckPointIndex = 1;
        SceneManager.LoadScene(1);
    }

    public void PlayCoopMode()
    {
        Options.CoopMode = true;
        Options.CheckPointIndex = 1;
        SceneManager.LoadScene(1);
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(Options.CheckPointIndex);
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void OpenSettings()
    {
        SceneManager.LoadScene(5);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeDifficulty(float new_value)
    {
        Options.difficulty = new_value / 10.0f;
    }
}
