using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ItemInterface<I> {

    void RemoveItem(Item item);
    void AddItem(Item item);
    bool CanAddItem();

}
