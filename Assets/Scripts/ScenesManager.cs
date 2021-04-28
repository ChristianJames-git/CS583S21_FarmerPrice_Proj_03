using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{


    public void onPlayGameButtonClicked()
    {
        SceneManager.LoadScene("GameScene");

    }

    public void onMainMenuButtonClicked()
    {

        SceneManager.LoadScene("MainMenu");
    }

    public void onHowToPlayButtonClicked()
    {

        SceneManager.LoadScene("HowToPlay");
    }
    public void onQuitClicked()
    {
        Debug.Log("On Quit Button Clicked.");

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif

    }
}