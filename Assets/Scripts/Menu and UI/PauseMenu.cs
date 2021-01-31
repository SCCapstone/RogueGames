using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject player;
    public GameObject health;
    public GameObject inventory;

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
    }

    // Function for the action of Pausing.
    void Pause() {
        Debug.Log("Pausing game...");
        pauseMenuUI.SetActive(true);
        player.GetComponent<Player>().enabled = false;
        health.SetActive(false);
        inventory.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    // Function for the Resume button.
    public void Resume() {
        Debug.Log("Resuming game...");
        pauseMenuUI.SetActive(false);
        player.GetComponent<Player>().enabled = true;
        health.SetActive(true);
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
