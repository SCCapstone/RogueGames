using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour {

    public static ItemAssets Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }


    public Transform pfItemWorld;

    public Sprite s_Sword_Steel;
    public Sprite s_HealthPotion;
    public Sprite s_Sword_Stone;
    public Sprite s_ArmorNone;
    public Sprite s_Armor_1;
    public Sprite s_Wood;
    public Sprite s_Stick;
    public Sprite s_Stone;
    public Sprite s_Steel;
    public Sprite s_Coal;
    public Sprite s_Torch;
    public Sprite s_String;
    public Sprite s_Bow;
    public Sprite s_Arrow;
    public Sprite s_Sword_Wood;
}
