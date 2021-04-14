using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenuIG : MonoBehaviour {
  public GameObject pauseMenuUI;
  public GameObject optionsMenuUI;
  public AudioMixer audioMixer;
  Resolution[] resolutions;
  public Dropdown resolutionDropdown;
  public Slider masterVol;
  public Slider bgmVol;
  public Slider sfxVol;

  void Start() {
    resolutions = Screen.resolutions;

    resolutionDropdown.ClearOptions();

    List<string> options = new List<string>();

    int currentResolutionIndex = 0;
    for (int i = 0; i < resolutions.Length; i++) {
      string option = resolutions[i].width + "x" + resolutions[i].height;
      options.Add(option);

      if (resolutions[i].width == Screen.currentResolution.width &&
          resolutions[i].height == Screen.currentResolution.height) {
        currentResolutionIndex = i;
      }
    }
    resolutionDropdown.AddOptions(options);
    resolutionDropdown.value = currentResolutionIndex;
    resolutionDropdown.RefreshShownValue();

    masterVol.value = PlayerPrefs.GetFloat("MasterVol", 0.75f);
    bgmVol.value = PlayerPrefs.GetFloat("BGMVol", 0.75f);
    sfxVol.value = PlayerPrefs.GetFloat("SFXVol", 0.75f);

  }

  void Update() {
    if (Input.GetKeyDown(KeyCode.Escape)) {
      PauseMenu();
    }
  }

  public void SetResolution(int resolutionIndex) {
    Resolution resolution = resolutions[resolutionIndex];
    Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    Debug.Log("Resolution set to " + resolution.width + " x " + resolution.height + ".");
  }

  public void SetMasterVolume(float volume) {

    audioMixer.SetFloat("volume_master", volume);
    PlayerPrefs.SetFloat("MasterVol", volume);
    Debug.Log("Master Volume = " + volume + ".");

  }

  public void SetBGMVolume(float volume) {

    audioMixer.SetFloat("volume_bgm", volume);
    PlayerPrefs.SetFloat("BGMVol", volume);
    Debug.Log("BGM Volume = " + volume + ".");

  }

  public void SetSFXVolume(float volume) {

    audioMixer.SetFloat("volume_sfx", volume);
    PlayerPrefs.SetFloat("SFXVol", volume);
    Debug.Log("SFX Volume = " + volume + ".");

  }

  public void SetFullScreen(bool isFullscreen) {
    Screen.fullScreen = isFullscreen;
    Debug.Log("Fullscreen = " + isFullscreen + ".");
  }

  public void PauseMenu() {
    Debug.Log("Pause menu loaded!");
    pauseMenuUI.SetActive(true);
    optionsMenuUI.SetActive(false);
  }

}
