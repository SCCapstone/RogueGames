using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject inventory;
    string scene;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
                if (GameIsPaused)
	        {
                        Resume();
                } else
                {
                        Pause();
                }
         }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (GameIsPaused)
            {
                Debug.Log("Loading Menu via key [1]...");
                scene = "Menu";
                GameIsPaused = false;
                Time.timeScale = 1f;
                SceneManager.LoadScene(scene);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (GameIsPaused)
            {
                Debug.Log("Quitting game via key [0]...");
                GameIsPaused = false;
                Application.Quit();
            }
        }
    }

    // Function for the action of Pausing.
    void Pause() {
        Debug.Log("Pausing game...");
        pauseMenuUI.SetActive(true);
        inventory.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    // Function for the Resume button.
    public void Resume() {
        Debug.Log("Resuming game...");
        pauseMenuUI.SetActive(false);
        inventory.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;   
    }

    // Function for the Menu button.
    public void LoadMenu() {
        Debug.Log("Loading Menu...");
        GameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    
    // Function for the Quit button.
    public void QuitGame() {
        Debug.Log("Quitting game...");
        GameIsPaused = false;
        Application.Quit();
    }
}
