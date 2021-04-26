using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
  public GameObject mainMenu;
  public GameObject craftingNotebook;

  // Function for the Play button. Starts the prototype.
  public void PlayGame() {
    Debug.Log("Starting game!");
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }

  public void Notebook() {
    Debug.Log("Notebook loaded!");
    craftingNotebook.SetActive(true);
    mainMenu.SetActive(false);
  }

  public void Options() {
    Debug.Log("Options menu loaded!");
    string scene = "OptionsMenu";
    SceneManager.LoadScene(scene);
  }

  // Function for the Quit button. Quits the game.
  public void QuitGame() {
    Debug.Log("Quitting game from Menu!");
    Application.Quit();
  }
}
