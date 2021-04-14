using UnityEngine;

public class Bow : Weapon {
  public GameObject arrowPrefab;

  public float attackDuration;
  private float _attackEndTime = 0f;

  private SpriteRenderer _spriteRenderer;

  public override void Attack(Vector3 attackDir) {
    if (durability > 0f && Time.time > _attackEndTime) {
      float arrowAngle = Mathf.Rad2Deg * Mathf.Atan2(attackDir.y, attackDir.x);
      GameObject arrowGO = Instantiate(arrowPrefab, transform.position,
          Quaternion.AngleAxis(arrowAngle, Vector3.forward)) as GameObject;
      arrowGO.GetComponent<Arrow>().shooter = transform.parent.gameObject;

      durability -= baseDegrade;
      _attackEndTime = Time.time + attackDuration;
    }
  }

  public void plusOneArrow() {
    durability += baseDegrade;
  }

  void Start() {
    _spriteRenderer = GetComponent<SpriteRenderer>();
    name = "Bow";
  }

  void Update() {
    if (durability < 0f)
      _spriteRenderer.color = Color.red;
    else
      _spriteRenderer.color = Color.white;
  }
}
