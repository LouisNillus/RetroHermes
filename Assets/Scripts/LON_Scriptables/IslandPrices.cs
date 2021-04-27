using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Island Prices", menuName ="Island Prices")]
public class IslandPrices : ScriptableObject
{
    public List<ItemPrice> prices = new List<ItemPrice>();

    public int FindItemPriceByName(ItemType name)
    {
        foreach(ItemPrice ip in prices)
        {
            if (name == ip.itemName)
            {
                return ip.price;
            }
        }

        return 0;
    }
}

[System.Serializable]
public class ItemPrice
{
    public ItemType itemName;
    public int price;
}
