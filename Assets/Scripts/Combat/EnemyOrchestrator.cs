using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrchestrator : MonoBehaviour {
  private const float _baseDifficulty = 0.5f;
  private float _targetDifficulty = _baseDifficulty;
  private float _currentDifficulty = 0.0f;

  private int _healthDeltaThreshold = 2;
  private int _lastHealth;
  private float _healthCheckTimer = 0.0f;
  private float _healthCheckInterval = 1.5f;

  private float _difficultyAdjustTimer = 0.0f;
  private float _difficultyAdjustInterval = 4.0f;

  private Player _player;
  private List<Enemy> _enemies;
  private List<Vector3> _spawns;

  public AudioSource enemyAudio;

  // Public API for enemy orchestration
  public GameObject SpawnEnemy(string name, Vector3 pos) {
    GameObject enemyGO = Instantiate(
        Resources.Load<GameObject>($"Enemies/{name}"), pos, Quaternion.identity) as GameObject;
    Enemy enemy = enemyGO.GetComponent<Enemy>();
    enemy.spawnPos = pos;
    _enemies.Add(enemy);
    enemyAudio.PlayOneShot(enemy.enemySpawnSFX, 0.3f);

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
    _lastHealth = _player.health;
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

  public int GetAttackerCount() {
    return _enemies.FindAll(e => e.attacking).Count;
  }

  public int GetDefenderCount() {
    return _enemies.FindAll(e => !e.attacking).Count;
  }

  public float AdjustDifficulty(float delta) {
    _targetDifficulty += delta;
    if (_targetDifficulty < 0.0f) _targetDifficulty = 0.0f;
    return _targetDifficulty;
  }

  List<Enemy> GetPotentialAttackers() {
    return _enemies.FindAll(e => (_currentDifficulty + e.difficulty <= _targetDifficulty) && !e.attacking);
  }

  List<Enemy> GetPotentialDefenders() {
    return _enemies.FindAll(e => e.attacking);
  }

  void PromoteEnemy() {
    List<Enemy> potentialAttackers = GetPotentialAttackers();

    if (potentialAttackers.Count == 0)
      return;

    int index = Random.Range(0, potentialAttackers.Count);

    potentialAttackers[index].attacking = true;
    _currentDifficulty += potentialAttackers[index].difficulty;
  }

  void DemoteEnemy() {
    List<Enemy> potentialDefenders = GetPotentialDefenders();

    if (potentialDefenders.Count == 0)
      return;

    int index = Random.Range(0, potentialDefenders.Count);

    potentialDefenders[index].attacking = false;
    _currentDifficulty -= potentialDefenders[index].difficulty;
  }

  void FixedUpdate() {
    // Filter dead enemies
    _enemies = _enemies.FindAll(e => e != null);
    if (_enemies.Count == 0) {
      _targetDifficulty = _baseDifficulty;
      _currentDifficulty = 0.0f;
      return;
    }

    //Debug.Log($"{_currentDifficulty} : {_targetDifficulty}");

    if (_healthCheckTimer < Time.time) {
      if (_player.health <= (_lastHealth - _healthDeltaThreshold))
        AdjustDifficulty(-0.2f);
      else if (_player.health >= (_lastHealth + _healthDeltaThreshold))
        AdjustDifficulty(0.2f);

      _lastHealth = _player.health;
      _healthCheckTimer = Time.time + _healthCheckInterval;
    }

    if (_difficultyAdjustTimer < Time.time) {
      if (_targetDifficulty > _baseDifficulty || _targetDifficulty < _baseDifficulty)
        _targetDifficulty = _baseDifficulty;

      _difficultyAdjustTimer = Time.time + _difficultyAdjustInterval;
    }

    while (_currentDifficulty > _targetDifficulty && GetPotentialDefenders().Count > 0) {
      DemoteEnemy();
    }

    while (_currentDifficulty < _targetDifficulty && GetPotentialAttackers().Count > 0) {
      PromoteEnemy();
    }

    foreach (Enemy enemy in _enemies) {
      if (enemy.health <= 0) {
        if (enemy.attacking)
          _currentDifficulty -= enemy.difficulty;

        enemyAudio.PlayOneShot(enemy.enemyDeathSFX, 0.3f);
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
