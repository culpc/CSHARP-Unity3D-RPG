using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    // Pause/Resume variables
    public static bool GameIsPause = false;

    public GameObject pauseMenuUI;


    //Scene Loaders

    // Play game
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
    }

    // Closes the game
    public void QuitGame()
    {
        Application.Quit();
    }

    // Load Main menu
    public void LoadMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }
}
