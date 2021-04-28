using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemData", menuName = "ItemData")]
public class ItemData : ScriptableObject
{
    public int maxStack;
    public Sprite sprite;
}

[System.Serializable]
public class Item
{
    public ItemData data;
    public int amount;
    public bool unlimitedStack;
    public ItemType itemName;
    public int price;

    

    public Item(int amount, bool unlimitedStack, ItemType itemName, int price)
    {
        this.amount = amount;
        this.unlimitedStack = unlimitedStack;
        this.itemName = itemName;
        this.price = price;
    }

    public Item()
    {

    }
}
