using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop instance;
    public IslandPrices islandPrices;
    public GameObject shopItemTemplate;
    

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        FillShopWithItems();
    }

    public void FillShopWithItems()
    {
        foreach(ItemPrice ip in islandPrices.prices)
        {
            GameObject go = Instantiate(shopItemTemplate, this.transform.position, Quaternion.identity);
            go.transform.SetParent(this.transform, false);
        }
    }
}
