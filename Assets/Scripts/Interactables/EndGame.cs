using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    public bool invent;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject health;
    public GameObject inventory;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.Alpha0)){
            if (GameIsPaused)
            {
                Debug.Log("Quitting game via key [0]...");
                GameIsPaused = false;
                Application.Quit();
            }
        }
    }


 	public void DoInteraction(){
 		 if (GameIsPaused)
        {
            Resume();
        }
        else
        {
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

 	void Pause()
    {
        Debug.Log("Pausing Game");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        health.SetActive(false);
        inventory.SetActive(false);
        player.GetComponent<Player>().enabled = false;
    }

    public void Resume()
    {
        Debug.Log("Resuming Game");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        health.SetActive(true);
        inventory.SetActive(true);
        player.GetComponent<Player>().enabled = true;
        GameIsPaused = false; 
    }

    public void QuitGame() {
        Debug.Log("Quitting game...");
        GameIsPaused = false;
        Application.Quit();
    }
}
