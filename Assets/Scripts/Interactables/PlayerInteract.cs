using UnityEngine;

public class PlayerInteract : MonoBehaviour {
  public GameObject currentInterObj = null;
  public interactionObject currentInterObjScript = null;
  public Inventory inventory;

  void Update() {
    if (Input.GetKeyDown(KeyCode.Q) && currentInterObj) {
      //if (currentInterObjScript.invent)
      //{
      //    inventory.AddItem(currentInterObj);
      //}
      Debug.Log("Sent Message");
      currentInterObj.SendMessage("DoInteraction");

      //currentInterObjScript = currentInterObj.GetComponentInChildren<>();
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
