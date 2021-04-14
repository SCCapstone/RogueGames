using UnityEngine;

public class FirstTimeControls : MonoBehaviour {
  public GameObject gameControls;
  public GameObject health;
  public GameObject inventory;
  public bool started = false;

  void Start() {
    Time.timeScale = 0f;
  }

  void Update() {
    if (!started && Input.anyKey) {
      gameControls.SetActive(false);
      health.SetActive(true);
      inventory.SetActive(true);
      Time.timeScale = 1f;
      started = true;
    }
  }
}
