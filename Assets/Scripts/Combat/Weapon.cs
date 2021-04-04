using UnityEngine;

public abstract class Weapon : MonoBehaviour {
  public float baseDegrade;
  public float durability = 1f;
  public AudioClip weaponSFX;

  public abstract void Attack(Vector3 attackDir);
}
