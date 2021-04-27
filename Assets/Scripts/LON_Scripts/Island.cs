using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    public List<ShopStock> shopStocks = new List<ShopStock>();
    public IslandPrices islandPrices;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) ShopSubscribe();
    }

    public void ShopSubscribe()
    {
        Shop.instance.currentIsland = this;
        Shop.instance.islandPrices = islandPrices;

        Shop.instance.FillShopWithItems();
    }



}

[System.Serializable]
public class ShopStock
{
    public int amount;
    public bool unlimitedStack;
    public ItemData data;

    public ShopStock(int amount, bool unlimitedStack, ItemData data)
    {
        this.amount = amount;
        this.unlimitedStack = unlimitedStack;
        this.data = data;
    }
}