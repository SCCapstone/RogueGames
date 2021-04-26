using UnityEngine;

public class TestMovement : MonoBehaviour {

  public float speed = 8.0f;

  void Start() {

  }

  void Update() {
    Vector2 movement_input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    Vector2 movement_delta = speed * movement_input.normalized * Time.deltaTime;
    transform.position += new Vector3(movement_delta.x, movement_delta.y, 0.0f);
  }
}
