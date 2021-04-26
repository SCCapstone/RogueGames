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

  private SpriteRenderer _spriteRenderer;

  private bool _retreat = false;

  IEnumerator DamageFlash() {
    Color tint = _spriteRenderer.color;
    _spriteRenderer.color = Color.grey;
    yield return new WaitForSeconds(0.1f);
    _spriteRenderer.color = tint;
  }

  public void Awake() {
    _spriteRenderer = GetComponent<SpriteRenderer>();
  }

  public override void TakeDamage(int damage) {
    health -= damage;
    StartCoroutine("DamageFlash");
  }

  public override void ActAggressive() {
    Vector3 impToPlayer = _playerGO.transform.position - transform.position;
    Vector3 movement = impToPlayer.normalized * speed * Time.fixedDeltaTime;

    if (impToPlayer.magnitude > attackDistance)
      _retreat = false;

    if (_retreat) {
      movement *= -attackSpeedMultiplier;
    } else if (impToPlayer.magnitude < attackDistance) {
      movement *= attackSpeedMultiplier;
    }

    rb.MovePosition(transform.position + movement);
  }

  public override void ActDefensive() {
    List<Vector3> impPositions = GetImpPositions();
    Vector3 impCenter = ComputeCenterOfMass(impPositions);
    Vector3 repulsion = ComputerRepulsion(impPositions, 0.3f);

    Vector3 impToTarget = impCenter - transform.position;
    Vector3 moveDir = impToTarget.normalized + (repulsion.normalized * 1.1f);

    Vector3 movement = moveDir * speed * Time.deltaTime;
    rb.MovePosition(transform.position + movement);
  }

  List<Vector3> GetImpPositions() {
    Imp[] imps = Object.FindObjectsOfType<Imp>();
    List<Vector3> positions = new List<Vector3>();

    for (int i = 0; i < imps.Length; i++) {
      if (imps[i] != this)
        positions.Add(imps[i].transform.position);
    }

    return positions;
  }

  Vector3 ComputeCenterOfMass(List<Vector3> positions) {
    Vector3 center = Vector3.zero;

    foreach (Vector3 pos in positions)
      center += pos;

    center /= positions.Count;

    return center;
  }

  Vector3 ComputerRepulsion(List<Vector3> positions, float repelDist) {
    Vector3 repulsion = Vector3.zero;
    float repelDistSqr = repelDist * repelDist;

    foreach (Vector3 pos in positions) {
      Vector3 impToOther = pos - transform.position;

      if (impToOther.sqrMagnitude < repelDistSqr)
        repulsion -= impToOther;
    }

    return repulsion;
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
  }

  void Update() {
  }
}
