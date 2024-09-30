using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject gameOverUI;
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Level 1");
    }

    public void MainMenuGUI()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadSceneAsync("Main menu");

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        // Restart the current scene (you might need to adjust this based on your scene setup)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

