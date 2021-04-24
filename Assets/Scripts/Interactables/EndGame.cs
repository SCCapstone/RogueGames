using UnityEngine;
using UnityEngine.SceneManagement;


public class EndGame : MonoBehaviour {
  // Start is called before the first frame update
  public bool invent;
  public static bool GameIsPaused = false;
  public GameObject pauseMenuUI;
  public GameObject health;
  public GameObject inventory;
  public GameObject player;
  public GameObject checkScreen;
  public GameObject endScreen;
  public GameObject dungeonObj;


  // Update is called once per frame
  void Update() {
    if (Input.GetKeyDown(KeyCode.Alpha0)) {
      if (GameIsPaused) {
        Debug.Log("Quitting game via key [0]...");
        GameIsPaused = false;
        Application.Quit();
      }
    }
  }


  public void DoInteraction() {
    if (GameIsPaused) {
      Resume();
    } else {
      Pause();
    }
  }

  /*
 	public void Load(){
 		GameObject dungeonObj = GameObject.FindGameObjectWithTag("Dungeon");
 		DungeonGeneration dungeonScript = dungeonObj.GetComponent<DungeonGeneration>();
 		if (dungeonScript.AllRoomsComplete() == true) {
 			GameObject finalLoad = GameObject.Instantiate(Resoures.Load(this.PrefabName())) as GameObject;
 		}
 	}
	*/

  void Pause() {
    Debug.Log("Pausing game...");
    checkScreen.SetActive(true);
    health.SetActive(false);
    inventory.SetActive(false);
    player.GetComponent<Player>().enabled = false;
    Time.timeScale = 0f;
    GameIsPaused = true;


    DungeonGeneration dungeonScript = dungeonObj.GetComponent<DungeonGeneration>();

    if (dungeonScript.AllRoomsComplete()) {
      endScreen.SetActive(true);
    }
  }

  public void Resume() {
    Debug.Log("Resuming game...");
    checkScreen.SetActive(false);
    health.SetActive(true);
    inventory.SetActive(true);
    player.GetComponent<Player>().enabled = true;
    Time.timeScale = 1f;
    GameIsPaused = false;
  }

  //close menu without pausing, used if player presses esc while menu is up
  public void closeMenu() {
    Debug.Log("closing menu...");
    checkScreen.SetActive(false);
    GameIsPaused = false;
  }

  public void LoadMenu() {
    Debug.Log("Loading Menu...");
    GameIsPaused = false;
    Time.timeScale = 1f;
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
  }

  public void QuitGame() {
    Debug.Log("Quitting game...");
    GameIsPaused = false;
    Application.Quit();
  }
}
