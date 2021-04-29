using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New ItemData", menuName = "ItemData")]
public class ItemData : ScriptableObject
{
    public int maxStack;
    [TextArea(2,3)]
    public string effect;
    public Sprite sprite;
}

[System.Serializable]
public class Item
{
    [Header("Item")]
    public ItemType itemName;
    public ItemData data;
    public int amount;
    [ReadOnly] public bool unlimitedStack;
    

    public Item(int amount, bool unlimitedStack, ItemType itemName)
    {
        this.amount = amount;
        this.unlimitedStack = unlimitedStack;
        this.itemName = itemName;
    }

    public Item()
    {

    }
}
