using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop instance;
    public IslandPrices islandPrices;
    public GameObject shopItemTemplate;
    public Island currentIsland;

    public List<GameObject> allItems = new List<GameObject>();
    

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //FillShopWithItems();
    }

    public void FillShopWithItems()
    {
        ClearShop();
        for (int i = 0; i < currentIsland.shopStocks.Count; i++)
        {
            GameObject go = Instantiate(shopItemTemplate, this.transform.position, Quaternion.identity);

            ShopItem si = go.GetComponent<ShopItem>();
            ItemPrice ip = currentIsland.shopStocks[i];

            si.slot.unlimitedStack = ip.unlimitedStack;
            si.slot.itemName = ip.itemName;
            si.slot.amount = ip.amount;

            allItems.Add(go);

            go.transform.SetParent(this.transform, false);
        }
    }

    public void ClearShop()
    {
        for (int i = 0; i < allItems.Count; i++)
        {
            Destroy(allItems[i]);
        }
    }

    public void SaveStocks()
    {
        currentIsland.shopStocks.Clear();
        for (int i = 0; i < allItems.Count; i++)
        {
            ShopItem si = allItems[i].GetComponent<ShopItem>();
            ItemPrice ip = new ItemPrice(si.slot.amount, si.slot.unlimitedStack, si.slot.itemName, islandPrices.FindItemPriceByName(si.slot.itemName));
            currentIsland.shopStocks.Add(ip);
        }
    }

    public void AddStock(ItemType itemName, int quantity)
    {
        foreach(GameObject stock in allItems)
        {
            if(stock.GetComponent<ShopItem>().slot.itemName != itemName)
            {
                continue;
            }
            else
            {
                stock.GetComponent<ShopItem>().slot.amount += quantity;
            }
        }
    }

}
