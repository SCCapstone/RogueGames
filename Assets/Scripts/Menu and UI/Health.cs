﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int numberOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public GameObject deathPopUp;
    public GameObject pauseCV;

    void Update() {
        for (int i = 0; i < hearts.Length; i++) {
            //Makes sure player health doesn't exceed the max number of hearts available.
            if (health > numberOfHearts) {
                health = numberOfHearts;
            }
            
            // Checks for current player health, denotes full if good, empty if bad.
            if (i < health) {
                hearts[i].sprite = fullHeart;
            } else {
                hearts[i].sprite = emptyHeart;
            }

            // Determines the number of max heart containers.
            // In this prototype, the absolute max is 5 hearts. Can reduce in Unity to 4,3,etc.
            if (i < numberOfHearts) {
                hearts[i].enabled = true;
            } else {
                hearts[i].enabled = false;
            }

            // Player has died, prompt them to restart.
            if (health == 0)
            {
                GetComponent<Player>().enabled = false;
                pauseCV.GetComponent<PauseMenu>().enabled = false;
                deathPopUp.SetActive(true);
                if (Input.GetKeyDown(KeyCode.R))
                {
                    GetComponent<Player>().enabled = true;
                    pauseCV.GetComponent<PauseMenu>().enabled = true;
                    deathPopUp.SetActive(false);
                }
            }

        }
    }
}
