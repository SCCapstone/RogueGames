using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imp : Enemy {
  public float speed;
  public int damage;
 
  private GameObject _playerGO;
  private Player _player;

  private Vector3 _randomWalkTarget;

  public override void TakeDamage(int damage) {
    health -= damage;
  }

  public override void ActAggressive() {
    Vector3 impToPlayer = _playerGO.transform.position - transform.position;
    transform.position += impToPlayer.normalized * speed * Time.deltaTime;
  }
  
  public override void ActDefensive() {
    Vector3 impToTarget = _randomWalkTarget - transform.position;
    Vector3 impToPlayer = _playerGO.transform.position - transform.position;
    Vector3 moveDir = impToTarget.normalized;

    // Avoid player in defensive mode
    if (impToPlayer.sqrMagnitude < 2f)
      moveDir = -impToPlayer.normalized;

    transform.position += moveDir * speed * Time.deltaTime;

    if (impToTarget.sqrMagnitude < 0.1f)
      SetRandomWalkTarget();
  }

  void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("Player"))
      _player.TakeDamage(damage);
  }

  void Start() {
    _playerGO = GameObject.FindGameObjectWithTag("Player");
    _player = _playerGO.GetComponent<Player>();
    SetRandomWalkTarget();
  }

  void Update() {
    if (health <= 0f)
      Destroy(gameObject);
  }

  void SetRandomWalkTarget() {
    _randomWalkTarget = new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), 0f);
  }
}
