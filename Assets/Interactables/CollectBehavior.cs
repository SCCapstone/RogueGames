using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBehavior : MonoBehaviour
{
    public bool invent;
    public static GameManager instance;
    public List<Item> itemList = new List<Item>();

    public void DoInteraction()
    {
        Debug.Log("Chest interacted");
        Item newItem = itemList[0];
        Inventory.instance.AddItem(Instantiate(newItem));
    }
}
