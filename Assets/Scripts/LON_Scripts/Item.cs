using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{

    public ItemData data;

    public string itemName;
    public int price;
    public int maxStack;

    // Start is called before the first frame update
    void Start()
    {
        itemName = data.itemName;
        price = data.price;
        maxStack = data.maxStack;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
