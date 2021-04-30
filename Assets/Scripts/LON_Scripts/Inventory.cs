using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Inventory : MonoBehaviour
{
    public List<Slot> inventorySlots = new List<Slot>();
    public GameObject slotTemplate;

    public AudioClip sellClip;
    public AudioClip buyClip;
    public AudioClip errorClip;


    public GameObject inventoryParent;
    [SerializeField] TextMeshProUGUI sellingPrice;
    [SerializeField] TextMeshProUGUI moneyCount;

    [Range(0,200)]
    [SerializeField] int yOffset; //In pixels
    [Range(0,200)]
    [SerializeField] int xOffset; //In pixels
    [Range(-1000,1000)]
    [SerializeField] int yMargin; //In pixels
    [Range(-1000,1000)]
    [SerializeField] int xMargin; //In pixels

    [SerializeField] int rows;
    [SerializeField] int columns;

    public int money = 0;
    public static Inventory instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitSlots();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Y)) EventSystem.current.SetSelectedGameObject(inventorySlots[0].gameObject);

        //DisplaySellingPrice();
        moneyCount.text = money.ToString() + "$";
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BuyItem();
            SellItem();
            Shop.instance.SaveStocks();
        }
    }

    public void SellItem()
    {
        GameObject g = EventSystem.current.currentSelectedGameObject;
        if (g != null && g.GetComponent<Slot>() != null)
        {
            Slot invSlot = g.GetComponent<Slot>();
            if (invSlot.item == null ||invSlot.IsEmpty() || Shop.instance.SellableHere(invSlot.item.itemName) == false || inventorySlots.Contains(invSlot) == false) return;
            else
            {

                Debug.Log("Can Sell");
                if (Shop.HasItemShop(invSlot.item.itemName, true)) //maybe false ?
                {
                    Shop.instance.AddStock(invSlot.item, 1, false);
                }
                else
                {
                    Shop.instance.AddStock(invSlot.item, 1, true);
                }

                MissionManager.instance.CheckShipping(invSlot.item.itemName);
                CargoManager.instance.RemoveCargo(invSlot.item.itemName);
                Earn(Shop.instance.islandPrices.FindItemPriceByName(invSlot.item.itemName));
                invSlot.item.amount--;
            }
        }
    }

    public void BuyItem()
    {
        GameObject g = EventSystem.current.currentSelectedGameObject;
        if (g != null && g.GetComponent<Slot>() != null)
        {
            Slot shopSlot = g.GetComponent<Slot>();
            if (shopSlot.item == null || shopSlot.IsEmpty() || inventorySlots.Contains(shopSlot) == true) return;
            else
            {
                Debug.Log("Can Buy");
                if(EnoughMoney(Shop.instance.islandPrices.FindItemPriceByName(shopSlot.item.itemName)))
                {
                    if(HasItem(shopSlot.item.itemName, true))
                    {
                        Debug.Log("Add more items");
                        GetItem(shopSlot.item.itemName).amount++;
                        shopSlot.item.amount--;
                    }
                    else if(AnySlotAvailable())
                    {
                        Slot invSlot = GetFirstAvailableSlot();
                        invSlot.item.data = shopSlot.item.data;
                        invSlot.item.itemName = shopSlot.item.itemName;

                        invSlot.item.amount++;
                        shopSlot.item.amount--;
                    }
                    else
                    {
                        Debug.Log("Exit");
                        return;
                    }

                    CargoManager.instance.AddCargo(shopSlot.item.itemName);
                    Pay(Shop.instance.islandPrices.FindItemPriceByName(shopSlot.item.itemName));
                }
            }
        }
    }




    public void InitSlots()
    {
        Vector3[] vecs = new Vector3[rows * columns];
        int a = 0;

        for (int i = 0; i < rows; i++)
        {

            for (int j = 0; j < columns; j++)
            {
                a++;
                vecs[i * j].y = i * -(slotTemplate.GetComponent<RectTransform>().rect.height + yOffset) + yMargin;
                vecs[i * j].x = j * (slotTemplate.GetComponent<RectTransform>().rect.width + xOffset) + xMargin;
                GameObject go = Instantiate(slotTemplate, vecs[i*j], Quaternion.identity);
                go.name += a;
                inventorySlots.Add(go.GetComponent<Slot>());
                go.transform.SetParent(inventoryParent.transform, false);
            }
        }

        EventSystem.current.SetSelectedGameObject(inventorySlots[0].gameObject);
    }

    public bool AnySlotAvailable()
    {
        foreach(Slot s in inventorySlots)
        {
            if (s.item.itemName == ItemType.Null) return true;
        }

        return false;
    }

    public Slot GetFirstAvailableSlot()
    {
        foreach (Slot s in inventorySlots)
        {
            if (s.item.itemName == ItemType.Null) return s;
        }

        return null;
    }

    public bool HasItem(ItemType item, bool checkMaxStack = false)
    {
        foreach (Slot s in inventorySlots)
        {
            if (s.item.itemName == item)
            {
                if (checkMaxStack && s.IsFull())
                {
                    continue;
                }
                else return true;
            }
        }

        return false;
    }

    public Item GetItem(ItemType item)
    {
        foreach (Slot s in inventorySlots)
        {
            if (s.item.itemName == item)
            {
                if (s.IsFull())
                {
                    continue;
                }
                else return s.item;
            }
        }

        return null;
    }

    public bool EnoughMoney(int price)
    {
        if (money - price >= 0) return true;
        else return false;
    }

    public void Pay(int price)
    {
        money -= price;
    }

    public void Earn(int value)
    {
        money += value;
    }

    public void DisplaySellingPrice()
    {
        GameObject g = EventSystem.current.currentSelectedGameObject;
        if (g != null && g.GetComponent<Slot>() != null)
        {
            Slot shopSlot = g.GetComponent<Slot>();
            if (shopSlot.item == null || shopSlot.IsEmpty() || inventorySlots.Contains(shopSlot) == false) return;
            else if(inventorySlots.Contains(shopSlot))
            {
                sellingPrice.text = "Selling Price : " + Shop.instance.islandPrices.FindItemPriceByName(shopSlot.item.itemName) + "$/unit";
            }
            else sellingPrice.text = "";
        }
    }
}
