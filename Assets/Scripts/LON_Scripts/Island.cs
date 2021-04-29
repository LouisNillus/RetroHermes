using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    public List<Item> shopStocks = new List<Item>();
    public IslandPrices islandPrices;

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