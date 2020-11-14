using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDoor : MonoBehaviour
{

    [SerializeField]
    string direction;

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            /*This script used for reference, Shouldn't be using it. 
             * GameObject dungeon = GameObject.FindGameObjectWithTag("Dungeon");
            DungeonGeneration dungeonGeneration = dungeon.GetComponent<DungeonGeneration> ();
            Room room = dungeonGeneration.CurrentRoom();
            dungeonGeneration.MoveToRoom(room.Neighbor(this.direction));
            SceneManager.LoadScene("RoomGenerator");
            */


        }

    }
}
