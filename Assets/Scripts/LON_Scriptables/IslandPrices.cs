using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Island Prices", menuName ="Island Prices")]
public class IslandPrices : ScriptableObject
{
    public List<ItemPrice> prices = new List<ItemPrice>();

    public int FindItemPriceByName(string name)
    {
        foreach(ItemPrice ip in prices)
        {
            if (name == ip.itemName)
            {
                Debug.Log("same names");
                return ip.price;
            }
        }

                Debug.Log("out");
        return 0;
    }
}

[System.Serializable]
public class ItemPrice
{
    public string itemName;
    public int price;
}
