using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
  public float speed;
  public float damage;
  public GameObject shooter;

  void Update() {
    transform.Translate(Vector3.right * speed * Time.deltaTime);
  }

  void OnTriggerEnter2D(Collider2D other) {
    Debug.Log(other.name);
    IDamageable damageable = other.GetComponent<IDamageable>();

    if (damageable != null && other.gameObject != shooter)
      damageable.TakeDamage(damage);

    if (other.gameObject != shooter && other.name != "RoomManager")
      Destroy(gameObject);
  }
}
