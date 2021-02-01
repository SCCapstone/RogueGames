using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests {
  public class PlayerBehaviourTests {

    [SetUp]
    public void SetupScene() {
      SceneManager.LoadScene("SampleScene");
    }

    [UnityTest]
    public IEnumerator TestCornerCollision() {
      const float setX = 0.8f;
      const float setY = 0.8f;
      GameObject player = GameObject.FindWithTag("Player");

      player.transform.position = new Vector3(setX, setY, 0f);

      yield return new WaitForSeconds(0.1f);

      Assert.That(player.transform.position.x, Is.LessThan(setX));
      Assert.That(player.transform.position.y, Is.LessThan(setY));
    }

  }
}
