using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameController : MonoBehaviour
{
    // Call this function to start the game and load the "Game" scene
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void RestartGame()
    {
        // You can restart the game by reloading the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Call this function to exit the game
    public void ExitGame()
    {
        #if UNITY_EDITOR
            // In the Unity Editor, stopping play mode
            UnityEditor.EditorApplication.isPlaying = false;
        #else
        // In a standalone build or a deployed build
        Application.Quit();
        #endif
    }
}
