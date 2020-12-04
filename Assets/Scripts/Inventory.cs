using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   public GameObject[] inventory = new GameObject[1];
   
    public void AddItem(GameObject item)
    {
        inventory[0] = item;
        Debug.Log(item.name + " was added");
    }
}
