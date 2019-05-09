using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace wig {
    public class MainMenu : MonoBehaviour
    {
        public GameObject player;
        public void Play()
        {
            Globals.isServer = true;
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
}
