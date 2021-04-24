using UnityEngine;

public class PlayerInteract : MonoBehaviour {
  public GameObject currentInterObj = null;
  public interactionObject currentInterObjScript = null;
  public Inventory inventory;
  public Player player;
  public GameObject pauseMenu;

  void Update() {
    //if player is dead
    if (player.health == 0) {
      return;
    }



    if ( Input.GetKeyDown(KeyCode.Q) && currentInterObj && !pauseMenu.activeSelf ) {
      //if (currentInterObjScript.invent)
      //{
      //    inventory.AddItem(currentInterObj);
      //}
      Debug.Log("Sent Message");
      currentInterObj.SendMessage("DoInteraction");


      //currentInterObjScript = currentInterObj.GetComponentInChildren<>();
    }
    if (Input.GetKeyDown(KeyCode.Escape) && currentInterObj) {
      currentInterObj.SendMessage("closeMenu");
      
      
    }
  }

  void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("interObject")) {
      Debug.Log(other.name);
      currentInterObj = other.gameObject;
      currentInterObjScript = currentInterObj.GetComponent<interactionObject>();
    }
  }

  void OnTriggerExit2D(Collider2D other) {
    if (other.CompareTag("interObject")) {
      if (other.gameObject == currentInterObj) {
        currentInterObj = null;
      }
    }
  }
}
