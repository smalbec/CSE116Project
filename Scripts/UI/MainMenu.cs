using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        SceneManager.LoadScene(2);
    }

    public void Quit()
    {
        // Quits the game. However it won't work on the editor
        Application.Quit();
    }
}
