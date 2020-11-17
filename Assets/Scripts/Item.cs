using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item {

    public enum ItemType {
        None,
        HealthPotion,
        ArmorNone,
        Armor_1,
        SwordNone,

        Wood,
        Steel,
        Coal, 
        String,
        Stick,
        Sword_Steel,
        Sword_Wood,
        Bow,
        Torch,
    }

    public ItemType itemType;
    public int amount = 1;
    private IItemHolder itemHolder;


    public void SetItemHolder(IItemHolder itemHolder) {
        this.itemHolder = itemHolder;
    }

    public IItemHolder GetItemHolder() {
        return itemHolder;
    }

    public void RemoveFromItemHolder() {
        if (itemHolder != null) {
            // Remove from current Item Holder
            itemHolder.RemoveItem(this);
        }
    }

    public void MoveToAnotherItemHolder(IItemHolder newItemHolder) {
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
        case ItemType.HealthPotion: return ItemAssets.Instance.s_HealthPotion;

        case ItemType.ArmorNone:    return ItemAssets.Instance.s_ArmorNone;
        case ItemType.Armor_1:      return ItemAssets.Instance.s_Armor_1;

        case ItemType.Wood:             return ItemAssets.Instance.s_Wood;
        case ItemType.Coal:             return ItemAssets.Instance.s_Coal;
        case ItemType.Stick:            return ItemAssets.Instance.s_Stick;
        case ItemType.Steel:          return ItemAssets.Instance.s_Steel;
        case ItemType.Sword_Wood:       return ItemAssets.Instance.s_Sword_Wood;
        case ItemType.Sword_Stone:    return ItemAssets.Instance.s_Sword_Stone;
        case ItemType.String:         return ItemAssets.Instance.s_String;
        case ItemType.Sword_Steel:         return ItemAssets.Instance.s_Sword_Steel;
        case ItemType.Bow:         return ItemAssets.Instance.s_Bow;
        case ItemType.Torch:         return ItemAssets.Instance.s_Torch;
        }
    }

    public Color GetColor() {
        return GetColor(itemType);
    }

    public static Color GetColor(ItemType itemType) {
        switch (itemType) {
        default:
        case ItemType.Sword:        return new Color(1, 1, 1);
        case ItemType.HealthPotion: return new Color(1, 0, 0);

        }
    }

    public bool IsStackable() {
        return IsStackable(itemType);
    }


    public static bool IsStackable(ItemType itemType) {
        switch (itemType) {
        default:
        case ItemType.HealthPotion:
            return true;
        case ItemType.Sword:
        case ItemType.SwordNone:
        case ItemType.ArmorNone:
            return false;

        case ItemType.Wood:
        case ItemType.Stick:
        case ItemType.Steel:
        case ItemType.Coal:
        case ItemType.Stone:
            return false;
        }
    }

    public int GetCost() {
        return GetCost(itemType);
    }

    public override string ToString() {
        return itemType.ToString();
    }

}
