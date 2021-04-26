using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests {
  public class DungeonGenTest {

    [SetUp]
    public void SetupScene() {
      SceneManager.LoadScene("SampleScene");
    }


    [UnityTest]
    public IEnumerator CheckRoomNum() {
      //iterates through the array of rooms and counts it up
      //checks if the number of rooms in the array actually is the desired number
      //techniclly a unit test
      
      GameObject dungeonObj = GameObject.FindGameObjectWithTag("Dungeon");
      DungeonGeneration generator = dungeonObj.GetComponent<DungeonGeneration>();

      Room[,] rooms = generator.getRoomArray();
      int roomsCounted = 0;

      for (int rowIndex = 0; rowIndex < rooms.GetLength(1); rowIndex++) {
        for (int columnIndex = 0; columnIndex < rooms.GetLength(0); columnIndex++) {
          if (rooms[columnIndex, rowIndex] != null) {
            roomsCounted += 1;
          }
        }
      }

      // starts game with controls menu
      GameObject uiObj = GameObject.FindGameObjectWithTag("InGameUI");
      FirstTimeControls firstTimeControlsScript = uiObj.GetComponent<FirstTimeControls>();
      firstTimeControlsScript.skipControls = true;

      yield return new WaitForSeconds(0.1f);

      Assert.AreEqual(roomsCounted, generator.numRooms);
    }





  }
}
