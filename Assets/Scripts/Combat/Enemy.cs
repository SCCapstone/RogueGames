using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable {
  public float difficulty;
  public bool attacking;
  public int health;
  public Vector3 spawnPos;
  public AudioClip enemySpawnSFX;
  public AudioClip enemyDeathSFX;
  public List<Item> itemList = new List<Item>();


  public abstract void ActAggressive();
  public abstract void ActDefensive();
  public abstract void TakeDamage(int damage);
}
