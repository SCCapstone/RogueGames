using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item {

    public enum ItemType {
        None,
        Sword_Wood,
        Bow,
        Arrow,
        Torch,
        Sword_Stone,
        Sword_Steel,
  

        String,
        Wood,
        Coal,
        Stick,
        Stone,
        Steel,
    }

    public ItemType itemType;
    public int amount = 1;
    private ItemInterface itemHolder;


    public void SetItemHolder(ItemInterface itemHolder) {
        this.itemHolder = itemHolder;
    }

    public ItemInterface GetItemHolder() {
        return itemHolder;
    }

    public void RemoveFromItemHolder() {
        if (itemHolder != null) {
            // Remove from current Item Holder
            itemHolder.RemoveItem(this);
        }
    }

    public void MoveToAnotherItemHolder(ItemInterface newItemHolder) {
        RemoveFromItemHolder();
        // Add to new Item Holder
        newItemHolder.AddItem(this);
    }



    public Sprite GetSprite() {
        return GetSprite(itemType);
    }

    public static Sprite GetSprite(ItemType itemType) {
        switch (itemType) {
        default:
        case ItemType.Sword_Wood:        return ItemAssets.Instance.s_Sword_Wood;
        case ItemType.Sword_Stone:       return ItemAssets.Instance.s_Sword_Stone;
        case ItemType.Sword_Steel:       return ItemAssets.Instance.s_Sword_Steel;
        case ItemType.Bow:               return ItemAssets.Instance.s_Bow;
        case ItemType.Torch:             return ItemAssets.Instance.s_Torch;
        case ItemType.Arrow:             return ItemAssets.Instance.s_Arrow;

        case ItemType.Wood:             return ItemAssets.Instance.s_Wood;
        case ItemType.Stick:            return ItemAssets.Instance.s_Stick;
        case ItemType.Stone:            return ItemAssets.Instance.s_Stone;
        case ItemType.Steel:            return ItemAssets.Instance.s_Steel;
        case ItemType.Coal:             return ItemAssets.Instance.s_Coal;
        case ItemType.String:           return ItemAssets.Instance.s_String;
        }
    }

    public bool IsStackable() {
        return IsStackable(itemType);
    }


    public static bool IsStackable(ItemType itemType) {
        switch (itemType) {
        default:
        case ItemType.Wood:
        case ItemType.Stone:
        case ItemType.Steel:
        case ItemType.String:
        case ItemType.Steel:
        case ItemType.Stick:
            return true;
        case ItemType.Sword_Wood:
        case ItemType.Sword_Stone:
        case ItemType.Bow:
        case ItemType.Sword_Steel:
        case ItemType.Torch:
            return false;
        }
    }


    public override string ToString() {
        return itemType.ToString();
    }

}
