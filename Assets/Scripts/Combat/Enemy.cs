using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable {
  public int health;
  public Vector3 spawnPos;
  
  public abstract void ActAggressive();
  public abstract void ActDefensive();
  public abstract void TakeDamage(int damage);
}
