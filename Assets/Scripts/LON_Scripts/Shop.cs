using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public static Shop instance;
    public IslandPrices islandPrices;
    public GameObject shopItemTemplate;
    public Island currentIsland;

    public TextMeshProUGUI islandNameText;

    public GameObject shopParent;
    public GameObject islandPanel;

    public GameObject wedontbuythis;

    public List<GameObject> allItems = new List<GameObject>();
    

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //FillShopWithItems();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            PlaneManager.instance.TakeOff();
            ShopState(false);
        }

        if(currentIsland != null)
        islandNameText.text = currentIsland.islandName;
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
            slot.item.unlimitedStack = true;

            allItems.Add(go);

            go.transform.SetParent(shopParent.transform, false);
        }
    }

    public void ClearShop()
    {
        for (int i = 0; i < allItems.Count; i++)
        {
            Destroy(allItems[i]);
        }
        allItems.Clear();
    }

    public void SaveStocks() //FULL PAS OPTI
    {
        if(currentIsland != null)
        {
            currentIsland.shopStocks.Clear();

            for (int i = 0; i < allItems.Count; i++)
            {
                ShopItem si = allItems[i].GetComponent<ShopItem>();
                Item ip = new Item(si.slot.item.amount, true, si.slot.item.itemName);
                ip.data = si.slot.item.data;
                currentIsland.shopStocks.Add(ip);
            }
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
                AudioManager.instance.PlaySFX(Inventory.instance.sellClip);
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
        

        slot.item = new Item(0, true, item.itemName);
        slot.item.data = item.data;

        allItems.Add(go);

        go.transform.SetParent(shopParent.transform, false);
    }

    public void ShopState(bool value)
    {
        islandPanel.SetActive(value);
    }

    public bool SellableHere(ItemType itemName)
    {
        foreach(ItemPrice ip in currentIsland.islandPrices.prices)
        {
            if (ip.itemName == itemName) return true;
        }

        WeDontButThis();
        return false;
    }

    public void WeDontButThis()
    {
        AudioManager.instance.PlaySFX(Inventory.instance.errorClip);
        StopAllCoroutines();
        StartCoroutine(ShowHide(wedontbuythis));
    }

    public IEnumerator ShowHide(GameObject go)
    {
        go.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        go.SetActive(false);
    }
}

