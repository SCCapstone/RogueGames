using UnityEngine;

public class AlterBehavior : MonoBehaviour {
  public bool invent;
  public static bool GameIsPaused = false;
  public GameObject pauseMenuUI;
  public GameObject health;
  public GameObject inventory;
  public GameObject player;
  public float generationChance = 0.5f;

  public void DoInteraction() {
    if (GameIsPaused) {
      Resume();
    } else {
      Pause();
    }
  }

  /*
  public void Load(){
      float currentRoomChance = Random.value;
      GameObject alter;
      if(currentRoomChance > generationChance){
          alter = GameObject.Instantiate(Resources.Load("Alter")) as GameObject;
          alter.transform.position = new Vector3(roomSize.x*(this.roomCoordinate.x-this.initialRoomCoordinate.x), -roomSize.y * (this.roomCoordinate.y - this.initialRoomCoordinate.y), 0);
      }
  }
  */

  void Pause() {
    Debug.Log("Pausing Game");
    pauseMenuUI.SetActive(true);
    Time.timeScale = 0f;
    GameIsPaused = true;
    health.SetActive(false);
    inventory.SetActive(false);
    player.GetComponent<Player>().enabled = false;
  }

  void Resume() {
    Debug.Log("Resuming Game");
    pauseMenuUI.SetActive(false);
    Time.timeScale = 1f;
    GameIsPaused = false;
    health.SetActive(true);
    inventory.SetActive(true);
    player.GetComponent<Player>().enabled = true;
    GameIsPaused = false;
  }
}
