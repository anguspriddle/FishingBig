using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneManager : MonoBehaviour
{
    // This method will be called when the "Start Game" button is clicked.
    public void StartGame()
    {
        SceneManager.LoadScene("MainGame"); // Load the "MainGame" scene.
    }

    // This method will be called when the "Quit" button is clicked.
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Exit play mode in the Unity Editor.
#else
        Application.Quit(); // Quit the application in a standalone build.
#endif
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
