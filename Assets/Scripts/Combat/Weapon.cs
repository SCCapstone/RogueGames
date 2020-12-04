using UnityEngine;

public abstract class Weapon : MonoBehaviour {
  public float baseDegrade;
  public float durability = 1f;

  public abstract void Attack(Vector3 attackDir);
}
