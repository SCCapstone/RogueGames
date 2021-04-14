using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsManager : MonoBehaviour {

  private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
  GameObject playerObj;
  Player playerScript;

  // Start is called before the first frame update
  void Start() {
    LoadKeybinds();
    playerObj = GameObject.FindGameObjectWithTag("Player");
    playerScript = playerObj.GetComponent<Player>();
  }

  void LoadKeybinds() {
    //theoretically, read in keyCodes and see what to bind it to. 
    keys.Add("Reset", KeyCode.R);
    keys.Add("ZoomOut", KeyCode.Minus);
    keys.Add("ZoomIn", KeyCode.Equals);
    keys.Add("CompleteRoom", KeyCode.C);
    keys.Add("GodModeToggle", KeyCode.G);
    keys.Add("GetCompletionStatus", KeyCode.Slash);
  }

  // Update is called once per frame
  void Update() {
    
    if (Input.GetKeyDown(keys["Reset"])) {
      //end/delete any current effects that might persist
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    if (Input.GetKey(keys["ZoomOut"])) {
      Camera.main.orthographicSize = Mathf.Min(9.0f, Camera.main.orthographicSize + 0.01f);
    }

    if (Input.GetKey(keys["ZoomIn"])) {
      Camera.main.orthographicSize = Mathf.Max(1.0f, Camera.main.orthographicSize - 0.01f);
    }

    // GODMODE
    if (Input.GetKeyDown(keys["GodModeToggle"])) {
      // makes the player invincible and allows them to walk through walls

      if (playerObj.layer == LayerMask.NameToLayer("Default")) {
        playerObj.layer = LayerMask.NameToLayer("GodMode");
        playerScript.baseSpeed = 3f;
      } else {
        playerObj.layer = LayerMask.NameToLayer("Default");
        playerScript.baseSpeed = 1f;
      }

      //
    }
    //Godmode commands
    if (playerObj.layer == LayerMask.NameToLayer("GodMode")) {
      GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
      playerObj.GetComponent<Player>().replenishWeapon("Sword", 1f);
      playerObj.GetComponent<Player>().replenishWeapon("Bow", 1f);



      if (Input.GetKeyDown(keys["CompleteRoom"])) {
        //A way to complete a room from outside. Might want to also make it delete all enemies?
        GameObject roomManager = GameObject.FindGameObjectWithTag("ActiveRoom");
        roomManager.GetComponent<RoomManager>().CompleteRoom();

      }



    }

    if (Input.GetKeyDown(keys["GetCompletionStatus"])) {
      // Prints to the Debug log wether all rooms have been completed
      // More of an example of how to use it than a debug feature

      GameObject dungeonObj = GameObject.FindGameObjectWithTag("Dungeon");
      DungeonGeneration dungeonScript = dungeonObj.GetComponent<DungeonGeneration>();

      Debug.Log("All Rooms Completed: " + dungeonScript.AllRoomsComplete());

    }

    
  }
}
