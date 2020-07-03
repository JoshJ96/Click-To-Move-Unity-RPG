using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
[System.Serializable]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        consumable,
        equipable,
        quest,
        material
    }
    public ItemType itemType;
    public string
        itemName,
        itemDescription;
    public Sprite itemImage;
    
}
