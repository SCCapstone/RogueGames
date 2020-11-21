using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ItemInterface {

    void RemoveItem(Item item);
    void AddItem(Item item);
    bool CanAddItem();

}
