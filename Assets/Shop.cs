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

            Slot slot = go.GetComponent<ShopItem>().slot;
            Item ip = currentIsland.shopStocks[i];

            slot.item = ip;

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
            Item ip = new Item(si.slot.item.amount, si.slot.unlimitedStack, si.slot.item.itemName, islandPrices.FindItemPriceByName(si.slot.item.itemName));
            currentIsland.shopStocks.Add(ip);
        }
    }

    public void AddStock(Item item, int quantity = 1, bool newStock = false)
    {
        if (newStock) NewSlotShop(item);

        foreach(GameObject stock in allItems)
        {
            if(stock.GetComponent<ShopItem>().slot.item.itemName != item.itemName)
            {
                continue;
            }
            else
            {
                stock.GetComponent<ShopItem>().slot.item.amount += quantity;
            }
        }
    }

    public static bool HasItemShop(ItemType item, bool checkMaxStack = false)
    {
        foreach (GameObject go in instance.allItems)
        {
            ShopItem si = go.GetComponent<ShopItem>();

            if (checkMaxStack && si.slot.locked) return false;

            if (si.slot.item.itemName == item) return true;
        }

        return false;
    }

    public static Item GetItemShop(ItemType item)
    {
        foreach (GameObject go in instance.allItems)
        {
            ShopItem si = go.GetComponent<ShopItem>();

            if (si.slot.item.itemName == item) return si.slot.item;
        }

        return null;
    }

    public void NewSlotShop(Item item)
    {
        GameObject go = Instantiate(shopItemTemplate, this.transform.position, Quaternion.identity);

        Slot slot = go.GetComponent<ShopItem>().slot;
        

        slot.item = new Item(item.amount, true, item.itemName, 0);
        slot.item.data = item.data;

        allItems.Add(go);

        go.transform.SetParent(this.transform, false);
    }

}
