using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterBehavior : MonoBehaviour
{
    public bool invent;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    public void DoInteraction()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }


    void Pause()
    {
        Debug.Log("Pausing Game");
        pauseMenuUI.SetActive(true);
        Time.timescale = 0f;
        GameIsPaused = true;
    }

    void Resume()
    {
        Debug.Log("Resuming Game");
        pauseMenuUI.SetActive(false);
        Time.timescale = 1f;
        GameIsPaused = false;
    }
}
