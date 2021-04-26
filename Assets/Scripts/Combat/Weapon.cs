using UnityEngine;

public abstract class Weapon : MonoBehaviour {
  public float baseDegrade;
  public float durability = 1f;
  public AudioClip weaponSFX;
  public string name;
  public abstract void Attack(Vector3 attackDir);

  public void Replenish(float val) {
    durability = Mathf.Min(durability + val, 1f);

  }
}