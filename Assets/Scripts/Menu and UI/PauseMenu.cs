﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
  public static bool GameIsPaused = false;
  public GameObject pauseMenuUI;
  public GameObject optionsMenuUI;
  public GameObject health;
  public GameObject inventory;
  public GameObject player;
  string scene;

  void Update() {
    if (Input.GetKeyDown(KeyCode.Escape)) {
      if (GameIsPaused) {
        if (pauseMenuUI.activeSelf) {
          Resume();
        }
      } else {
        Pause();
      }

    }

    /*
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
    */
  }

  // Function for the action of Pausing.
  void Pause() {
    Debug.Log("Pausing game...");
    pauseMenuUI.SetActive(true);
    health.SetActive(false);
    inventory.SetActive(false);
    player.GetComponent<Player>().enabled = false;
    Time.timeScale = 0f;
    GameIsPaused = true;
  }

  // Function for the Resume button.
  public void Resume() {
    Debug.Log("Resuming game...");
    pauseMenuUI.SetActive(false);
    health.SetActive(true);
    inventory.SetActive(true);
    player.GetComponent<Player>().enabled = true;
    Time.timeScale = 1f;
    GameIsPaused = false;
  }

  public void Options() {
    Debug.Log("Loading Options...");
    pauseMenuUI.SetActive(false);
    optionsMenuUI.SetActive(true);
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
