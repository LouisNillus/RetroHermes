using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Island Prices", menuName ="Island Prices")]
public class IslandPrices : ScriptableObject
{
    public List<Item> prices = new List<Item>();

    public int FindItemPriceByName(ItemType name)
    {
        foreach(Item item in prices)
        {
            if (name == item.itemName)
            {
                return item.price;
            }
        }

        return 0;
    }
}
