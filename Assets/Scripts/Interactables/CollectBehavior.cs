using System.Collections.Generic;
using UnityEngine;

public class CollectBehavior : MonoBehaviour {
  public bool invent;
  public static GameManager instance;
  public List<Item> itemList = new List<Item>();
  public float generationChance = 0.5f;

  public void DoInteraction() {
    Debug.Log("Chest interacted");
    Item newItem = itemList[0];
    Inventory.instance.AddItem(Instantiate(newItem));
  }

  /*
 public void Load(){
     float currentRoomChance = Random.value;
     GameObject chest;
     if(currentRoomChance > generationChance){
         chest = GameObject.Instantiate(Resources.Load("Alter")) as GameObject;
         chest.transform.position = new Vector3(roomSize.x*(this.roomCoordinate.x-this.initialRoomCoordinate.x), -roomSize.y * (this.roomCoordinate.y - this.initialRoomCoordinate.y), 0);
     }
 }
 */
}
