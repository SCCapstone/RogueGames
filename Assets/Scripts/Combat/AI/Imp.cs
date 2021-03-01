using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imp : Enemy {
  public float speed;
  public int damage;
  public float attackDistance;
  public float attackSpeedMultiplier;
 
  private GameObject _playerGO;
  private Player _player;

  private Rigidbody2D _rigidbody;
  private Vector3 _randomWalkTarget;
  private bool _retreat = false;

  public override void TakeDamage(int damage) {
    health -= damage;
  }

  public override void ActAggressive() {
    Vector3 impToPlayer = _playerGO.transform.position - transform.position;
    //transform.position += impToPlayer.normalized * speed * Time.deltaTime;
    Vector3 movement = impToPlayer.normalized * speed * Time.fixedDeltaTime;

    if (impToPlayer.magnitude > attackDistance)
      _retreat = false;

    if (_retreat) {
      movement *= -attackSpeedMultiplier;
    }
    else if (impToPlayer.magnitude < attackDistance) {
      movement *= attackSpeedMultiplier;
    }

    _rigidbody.MovePosition(transform.position + movement);
  }
  
  public override void ActDefensive() {
    Vector3 impToTarget = _randomWalkTarget - transform.position;
    Vector3 impToPlayer = _playerGO.transform.position - transform.position;
    Vector3 moveDir = impToTarget.normalized;

    // Avoid player in defensive mode
    //if (impToPlayer.sqrMagnitude < 2f)
    //  moveDir = -impToPlayer.normalized;

    //transform.position += moveDir * speed * Time.deltaTime;
    Vector3 movement = moveDir * speed * Time.deltaTime;
    _rigidbody.MovePosition(transform.position + movement);

    if (impToTarget.sqrMagnitude < 0.1f)
      SetRandomWalkTarget();
  }

  void OnCollisionEnter2D(Collision2D col) {
    if (col.gameObject.CompareTag("Player")) {
      _player.TakeDamage(damage);
      _retreat = true;
    }
  }

  void Start() {
    _playerGO = GameObject.FindGameObjectWithTag("Player");
    _player = _playerGO.GetComponent<Player>();
    _rigidbody = GetComponent<Rigidbody2D>();
    SetRandomWalkTarget();
  }

  void Update() {
    if (health <= 0f)
      Destroy(gameObject);
  }

  void SetRandomWalkTarget() {
    const float walkRange = 1f;
    _randomWalkTarget = spawnPos + new Vector3(Random.Range(-walkRange, walkRange), Random.Range(-walkRange, walkRange), 0f);
  }
}
