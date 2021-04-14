using UnityEngine;

public class FallenAngel : Enemy {
  public GameObject arrowPrefab;
  public float speed;
  public int meleeDamage;
  public float shootFrequency;
  public float defensiveDistance;
  public float aggressiveDistance;

  private GameObject _playerGO;
  private Player _player;
  private float _nextShootTime = 0f;

  private float _targetDistance;

  public override void TakeDamage(int damage) {
    health -= damage;
  }

  public override void ActAggressive() {
    _targetDistance = aggressiveDistance;

    if (Time.time > _nextShootTime) {
      Vector3 fallenToPlayer = _playerGO.transform.position - transform.position;

      float arrowAngle = Mathf.Rad2Deg * Mathf.Atan2(fallenToPlayer.y, fallenToPlayer.x);
      GameObject arrowGO = Instantiate(arrowPrefab, transform.position,
          Quaternion.AngleAxis(arrowAngle, Vector3.forward)) as GameObject;
      arrowGO.GetComponent<Arrow>().shooter = gameObject;
      _nextShootTime = Time.time + 1/shootFrequency;
    }
  }

  public override void ActDefensive() {
    _targetDistance = defensiveDistance;
  }

  void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("Player"))
      _player.TakeDamage(meleeDamage);
  }

  void Awake() {
    _playerGO = GameObject.FindGameObjectWithTag("Player");
    _player = _playerGO.GetComponent<Player>();
  }

  void FixedUpdate() {
    Vector3 fallenToPlayer = _playerGO.transform.position - transform.position;
    float fallenToPlayer_dist = fallenToPlayer.magnitude;
    Vector3 moveDir = fallenToPlayer.normalized;

    if (fallenToPlayer_dist < _targetDistance)
      moveDir = -fallenToPlayer.normalized;

    float speed_multiplier = 0.2f * Mathf.Pow(fallenToPlayer_dist - defensiveDistance, 2f);

    Vector3 movement = moveDir * speed*speed_multiplier * Time.fixedDeltaTime;
    rb.MovePosition(transform.position + movement);
    //transform.position += moveDir * (speed * speed_multiplier) * Time.deltaTime;
    Debug.Log(Time.timeScale);

  }
}
