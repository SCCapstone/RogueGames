﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable {
  public Animator animator;
  public Rigidbody2D rb;

  // Health
  public int health = 5;
  private float _damageColorEndTime;
  private float _damageColorDuration = 0.1f;
  private Health _healthUI;

  // Movement
  public float baseSpeed = 5f;
  private Vector3 _moveDir;
  private float _speed;

  // Dodging
  public float dodgeMultiplier = 2.5f;
  public float dodgeCooldown = 1.5f;
  public float dodgeDuration = 0.3f;
  public AnimationCurve dodgeSpeedCurve;
  private float _dodgeRefreshTime;
  private float _dodgeEndTime;
  private Vector3 _dodgeDir;
  private bool _dodging;

  // Weapons
  public GameObject _primaryWeaponGO;
  public GameObject _secondaryWeaponGO;
  private bool _primaryActive = true;
  private Weapon _activeWeapon;
  private Weapon _inactiveWeapon;

  // Debug
  private LineRenderer _lineRenderer;
  private SpriteRenderer _spriteRenderer;
  private bool _useRawInput = false;

  public void TakeDamage(int damage) {
    if (_dodging)
      return;
    
    health -= damage;
    _damageColorEndTime = Time.time + _damageColorDuration;
  }

  void Start() {
    _lineRenderer = GetComponent<LineRenderer>();
    _spriteRenderer = GetComponent<SpriteRenderer>();
    _healthUI = GetComponent<Health>();
    _activeWeapon = _primaryWeaponGO.GetComponent<Weapon>();
    _inactiveWeapon = _secondaryWeaponGO.GetComponent<Weapon>();
    _secondaryWeaponGO.SetActive(false);
  }

  void FixedUpdate() {
    // Move player and set animator parameters
    Vector3 movement = _moveDir * _speed * Time.fixedDeltaTime;
    rb.MovePosition(transform.position + movement);
    //transform.position += moveDir * speed * Time.deltaTime;
  }

  void Update() {
    // Handle damage
    if (Time.time < _damageColorEndTime)
      _spriteRenderer.color = Color.red;
    else
      _spriteRenderer.color = Color.white;

    if (health <= 0f) {
      transform.position = Vector3.zero;
      _spriteRenderer.color = Color.red;
    }

    // Dodge and movement
    Vector2 moveInput;

    if (_useRawInput)
      moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); 
    else
      moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); 

    _moveDir = new Vector3(moveInput.x, moveInput.y, 0f);
    _moveDir = Vector3.ClampMagnitude(_moveDir, 1f);

    if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > _dodgeRefreshTime && _moveDir.sqrMagnitude > 0f) {
      _dodging = true;
      _dodgeDir = _moveDir.normalized;
      _dodgeRefreshTime = Time.time + dodgeCooldown;
      _dodgeEndTime = Time.time + dodgeDuration;
    }
    
    _speed = baseSpeed;

    if (_dodging) {
      _moveDir = _dodgeDir;
      float t = 1f - (_dodgeEndTime - Time.time) / dodgeDuration;
      
      if (t > 1f)
        _dodging = false;
      else
        _speed *= dodgeMultiplier * dodgeSpeedCurve.Evaluate(t);
    }

    animator.SetFloat("Horizontal", moveInput.x);
    animator.SetFloat("Vertical", moveInput.y);
    animator.SetFloat("Magnitude", Vector3.ClampMagnitude(moveInput, 1f).magnitude);

    // Aiming and orientation
    Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    mouseWorldPos.z = 0f;

    Vector3 playerToMouseDir = (mouseWorldPos - transform.position).normalized;
    float orient = Mathf.Sign(playerToMouseDir.x);
    transform.localScale = new Vector3(orient, transform.localScale.y, transform.localScale.z);

    _lineRenderer.SetPosition(0, transform.position + 0.1f * playerToMouseDir);
    _lineRenderer.SetPosition(1, transform.position + 0.3f * playerToMouseDir);

    // Attacking
    if (Input.GetMouseButtonDown(0))
      _activeWeapon.Attack(playerToMouseDir);

    // Switch weapons
    if (Input.GetMouseButtonDown(1)) {
      _primaryActive = !_primaryActive;

      Weapon tmp = _activeWeapon;
      _activeWeapon = _inactiveWeapon;
      _inactiveWeapon = tmp;

      if (_primaryActive) {
        _primaryWeaponGO.SetActive(true);
        _secondaryWeaponGO.SetActive(false);
      }
      else {
        _primaryWeaponGO.SetActive(false);
        _secondaryWeaponGO.SetActive(true);
      }
    }

    _healthUI.health = health;
  }

/*  void OnGUI() {
    GUI.Box(new Rect(10, 10, 240, 200), "Dev Menu");
    _useRawInput = GUI.Toggle(new Rect(20, 40, 80, 20), _useRawInput, "Raw Input");

    GUI.Label(new Rect(20, 100, 200, 20), "Weapon Durability: " + _activeWeapon.durability.ToString("n2"));
    if (GUI.Button(new Rect(20, 120, 120, 20), "Restore Durability"))
      _activeWeapon.durability = 1.0f;

    GUI.Label(new Rect(20, 140, 120, 20), "Health: " + health.ToString("n1"));

    if (GUI.Button(new Rect(20, 160, 120, 20), "Restore Health")) {
      health = 8f;
      _spriteRenderer.color = Color.white;
    }
  }
*/
}