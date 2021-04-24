using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests {
  public class RoomManagerTest {
    [SetUp]
    public void SetupScene() {
      SceneManager.LoadScene("SampleScene");
    }


    [UnityTest]
    public IEnumerator CheckRoomNum() {
      //behavioral test
      // Follow a similar test as playerBehaviourTests.TestCornerCollision()
      // spawn doors of start room and try to get past them
      // if the player is pushed back out of them (like the corners)
      // the the doors spawned correctly

      GameObject dungeonObj = GameObject.FindGameObjectWithTag("Dungeon");
      DungeonGeneration generator = dungeonObj.GetComponent<DungeonGeneration>();
      Room startRoom = generator.getStartRoom();

      string nesw = startRoom.PrefabName().Substring(11);

      GameObject player = GameObject.FindWithTag("Player");

      Vector2 movement = new Vector2(0f, 0f);
      const float doorPos = 0.95f;

      if (nesw.Contains("N")) {
        movement.y = doorPos;
      } else if (nesw.Contains("E")) {
        movement.x = doorPos;
      } else if (nesw.Contains("S")) {
        movement.y = -doorPos;
      } else if (nesw.Contains("W")) {
        movement.x = -doorPos;
      }
      startRoom.placeDoors();

      player.transform.position = new Vector3(movement.x, movement.y, 0f);

      // starts game with controls menu
      GameObject uiObj = GameObject.FindGameObjectWithTag("InGameUI");
      FirstTimeControls firstTimeControlsScript = uiObj.GetComponent<FirstTimeControls>();
      firstTimeControlsScript.skipControls = true;

      //new WaitForSeconds(0.5f);

      //Debug.Log("Player Position Magnitude: " + player.transform.position.magnitude);

      yield return new WaitForSeconds(0.1f);

      Assert.That(movement.magnitude, Is.GreaterThan(0f));
      Assert.That(Mathf.Abs(player.transform.position.x), Is.LessThan(doorPos));
      Assert.That(Mathf.Abs(player.transform.position.y), Is.LessThan(doorPos));



    }

  }
}
