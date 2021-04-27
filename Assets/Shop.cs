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
            ShopStock ss = currentIsland.shopStocks[i];

            si.slot.unlimitedStack = ss.unlimitedStack;
            si.slot.item = ss.data;
            si.slot.amount = ss.amount;

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
            ShopStock ss = new ShopStock(si.slot.amount, si.slot.unlimitedStack, si.slot.item);
            currentIsland.shopStocks.Add(ss);
        }
    }

}
