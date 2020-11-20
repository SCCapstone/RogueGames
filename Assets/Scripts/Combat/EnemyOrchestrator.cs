using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrchestrator : MonoBehaviour {
  private Player _player;
  private List<Enemy> _enemies;
  private List<Vector3> _spawns;

  public void SpawnEnemy(string name) {
    Vector3 spawnPos = _spawns[Random.Range(0, _spawns.Count)];
    GameObject impGO = Instantiate(
        Resources.Load<GameObject>($"Enemies/{name}"), spawnPos, Quaternion.identity) as GameObject;
    _enemies.Add(impGO.GetComponent<Enemy>());
  }

  public void Awake() {
    _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    _enemies = new List<Enemy>();
    _spawns = new List<Vector3>();

    Transform enemySpawns = GameObject.Find("enemy_spawns").transform;
    foreach (Transform spawn in enemySpawns) {
      _spawns.Add(spawn.position);
    }
  }

  void Update() {
    // Filter dead enemies
    _enemies = _enemies.FindAll(e => e != null);

    // First enemy is aggressive, others are defensive
    for (int i = 0; i < _enemies.Count; i++) {
      Enemy enemy = _enemies[i];

      if (i == 0 && _player.health > 0f)
        enemy.ActAggressive();
      else
        enemy.ActDefensive();
    }
  }

  void OnGUI() {
    if (GUI.Button(new Rect(20, 60, 100, 20), "Spawn Imp")) {
      SpawnEnemy("imp");
    }

    if (GUI.Button(new Rect(20, 80, 100, 20), "Spawn Fallen")) {
      SpawnEnemy("fallen");
    }
  }
}
