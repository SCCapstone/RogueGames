using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrchestrator : MonoBehaviour {
  public const float targetDifficulty = 0.5f;
  private float _currentDifficulty = 0.0f;

  private Player _player;
  private List<Enemy> _enemies;
  private List<Vector3> _spawns;

  // Public API for enemy orchestration
  public GameObject SpawnEnemy(string name, Vector3 pos) {
    GameObject enemyGO = Instantiate(
        Resources.Load<GameObject>($"Enemies/{name}"), pos, Quaternion.identity) as GameObject;
    Enemy enemy = enemyGO.GetComponent<Enemy>();
    enemy.spawnPos = pos;
    _enemies.Add(enemy);

    return enemyGO;
  }

  public GameObject SpawnEnemy(string name) {
    Vector3 spawnPos = _spawns[Random.Range(0, _spawns.Count)];
    return SpawnEnemy(name, spawnPos);
  }

  public int GetLiveEnemies() {
    return _enemies.Count;
  }

  public void Awake() {
    _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    _enemies = new List<Enemy>();
    _spawns = new List<Vector3>();

    GameObject enemySpawnsGO = GameObject.Find("enemy_spawns") as GameObject;

    if (enemySpawnsGO) {
      Transform enemySpawns = enemySpawnsGO.transform;

      foreach (Transform spawn in enemySpawns) {
        _spawns.Add(spawn.position);
      }
    }
  }

  int GetAttackerCount() {
    return _enemies.FindAll(e => e.attacking).Count;
  }

  int GetDefenderCount() {
    return _enemies.FindAll(e => !e.attacking).Count;
  }

  void PromoteEnemy() {
    if (GetDefenderCount() == 0)
      return;

    int index;

    do {
      index = Random.Range(0, _enemies.Count);
    } while (_enemies[index].attacking);

    _enemies[index].attacking = true;
    _currentDifficulty += _enemies[index].difficulty;
  }

  void DemoteEnemy() {
    if (GetAttackerCount() == 0)
      return;

    int index;

    do {
      index = Random.Range(0, _enemies.Count);
    } while (!_enemies[index].attacking);

    _enemies[index].attacking = false;
    _currentDifficulty -= _enemies[index].difficulty;
  }

  void FixedUpdate() {
    // Filter dead enemies
    _enemies = _enemies.FindAll(e => e != null);
    Debug.Log(_currentDifficulty);

    while (_currentDifficulty < targetDifficulty && GetDefenderCount() > 0) {
      PromoteEnemy();
    }

    foreach (Enemy enemy in _enemies) {
      if (enemy.health <= 0) {
        if (enemy.attacking)
          _currentDifficulty -= enemy.difficulty;

        Destroy(enemy.gameObject);
        continue;
      }

      if (enemy.attacking)
        enemy.ActAggressive();
      else
        enemy.ActDefensive();
    }
  }

}
