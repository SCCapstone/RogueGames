using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests {
    public class PlayerUnitTests {

        [SetUp]
        public void SetupScene()
        {
            SceneManager.LoadScene("SampleScene");
        }

        [Test]
        public void TestTakeDamage()
        {
            const int damagedHealth = 4;
            const int damage = 1;
            GameObject player = GameObject.FindWithTag("Player");

            player.GetComponent<Player>().TakeDamage(damage);

            Assert.AreEqual(player.GetComponent<Player>().health, damagedHealth);
        }

    }
}

