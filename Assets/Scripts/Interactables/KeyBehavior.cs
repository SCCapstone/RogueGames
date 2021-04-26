using UnityEngine;

public class interactionObject : MonoBehaviour {
  public bool invent;
  public float generationChance = 0.5f;

  public void DoInteraction() {
    gameObject.SetActive(false);
  }
  /*
 public void Load(){
     float currentRoomChance = Random.value;
     GameObject key;
     if(currentRoomChance > generationChance){
         key = GameObject.Instantiate(Resources.Load("Alter")) as GameObject;
         key.transform.position = new Vector3(roomSize.x*(this.roomCoordinate.x-this.initialRoomCoordinate.x), -roomSize.y * (this.roomCoordinate.y - this.initialRoomCoordinate.y), 0);
     }
 }
 */
}
