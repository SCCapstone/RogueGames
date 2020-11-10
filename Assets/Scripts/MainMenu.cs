using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Function for the Play button. Starts the prototype.
    public void PlayGame() {
        Debug.Log("Starting game!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    // Function for the Quit button. Quits the game.
    public void QuitGame() {
    	Debug.Log("Quitting game from Menu!");
    	Application.Quit();
    }
}
