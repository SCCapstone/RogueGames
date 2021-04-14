using UnityEngine;

public class FirstTimeControls : MonoBehaviour {
  public GameObject gameControls;
  public GameObject health;
  public GameObject inventory;

  void Start() {
    Time.timeScale = 0f;
  }

  void Update() {
    if (Input.anyKey) {
      gameControls.SetActive(false);
      health.SetActive(true);
      inventory.SetActive(true);
      Time.timeScale = 1f;
    }
  }
}
