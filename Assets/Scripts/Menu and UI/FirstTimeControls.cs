using UnityEngine;

public class FirstTimeControls : MonoBehaviour {
  public GameObject gameControls;
  public GameObject health;
  public GameObject inventory;
  public bool started = false;
  public bool skipControls = false;

  void Start() {
    Time.timeScale = 0f;
    health.SetActive(false);
    inventory.SetActive(false);
    }

  void Update() {
    if ( !started && (Input.anyKey || skipControls)) {
      gameControls.SetActive(false);
      health.SetActive(true);
      inventory.SetActive(true);
      Time.timeScale = 1f;
      started = true;
    }
  }
}
