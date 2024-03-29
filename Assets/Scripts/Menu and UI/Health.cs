﻿using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
  public int health;
  public int numberOfHearts;
  public bool audioHasPlayed;

  public Image[] hearts;
  public Sprite fullHeart;
  public Sprite emptyHeart;
  public GameObject deathPopUp;
  public GameObject pauseCV;
  public AudioSource playerAudio;
  public AudioClip deathSFX;

  void Start() {
    audioHasPlayed = false;
    GetComponent<Player>().enabled = true;
    pauseCV.GetComponent<PauseMenu>().enabled = true;
    deathPopUp.SetActive(false);
  }

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
      if (health == 0) {
        if (!audioHasPlayed) {
          playerAudio.PlayOneShot(deathSFX, 0.25f);
          audioHasPlayed = true;
        }
        //playerAudio.clip = deathSFX;
        //playerAudio.Play();
        GetComponent<Player>().enabled = false;
        pauseCV.GetComponent<PauseMenu>().enabled = false;
        deathPopUp.SetActive(true);
      }

    }
  }
}
