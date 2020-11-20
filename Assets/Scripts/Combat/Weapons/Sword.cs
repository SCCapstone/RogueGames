using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Sword : Weapon {
  public float damage;
  public float attackReach;
  public float attackDuration;
  public AnimationCurve swingCurve;

  private bool _attacking = false;
  private float _attackEndTime = 0f;
  private Vector2 _initPos;
  private Quaternion _initRot;
  private Vector3 _attackDir;

  private SpriteRenderer _spriteRenderer;

  public override void Attack(Vector3 attackDir) {
    if (durability > 0f) {
      _attacking = true;
      _attackDir = attackDir;
      durability -= baseDegrade;
      _attackEndTime = Time.time + attackDuration;
    }
  }

  void Start() {
    _initPos = transform.localPosition;
    _initRot = transform.localRotation;
    _spriteRenderer = GetComponent<SpriteRenderer>();
  }

  void Update() {
    if (Time.time < _attackEndTime) {
      float t = 1f - (_attackEndTime - Time.time) / attackDuration;
      float orient = transform.parent.localScale.x;
      float rotOrient = orient < 0f ? Mathf.PI : 0f;
      transform.eulerAngles = new Vector3(
          0f, 0f,
          Mathf.Rad2Deg * (rotOrient + Mathf.Atan2(_attackDir.y, _attackDir.x))
      );
      transform.position = transform.parent.position + (_attackDir * attackReach) * swingCurve.Evaluate(t);
    }
    else {
      transform.localPosition = _initPos;
      transform.localRotation = _initRot;
      _attacking = false;
    }

    if (durability < 0f)
      _spriteRenderer.color = Color.red;
    else
      _spriteRenderer.color = Color.white;
  }

  void OnTriggerEnter2D(Collider2D other) {
    Enemy hitEnemy = other.gameObject.GetComponent<Enemy>();

    if (_attacking && hitEnemy != null) {
      hitEnemy.TakeDamage(damage);
    }
  }

}
