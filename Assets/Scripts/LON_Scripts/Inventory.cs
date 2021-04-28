using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public List<Slot> inventorySlots = new List<Slot>();
    public GameObject slotTemplate;

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


    // Start is called before the first frame update
    void Start()
    {
        InitSlots();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BuyItem();
            SellItem();
        }
    }

    public void SellItem()
    {
        GameObject g = EventSystem.current.currentSelectedGameObject;
        if (g != null && g.GetComponent<Slot>() != null)
        {
            Slot slot = g.GetComponent<Slot>();

            if (slot.IsEmpty() || inventorySlots.Contains(slot) == false) return;
            else
            {
                if (HasItem(slot.item.itemName, true))
                {
                    GetItem(slot.item.itemName).amount++;
                    slot.item.amount--;
                }
                else if (AnySlotAvailable())
                {
                    Item item = GetFirstAvailableSlot().item;
                    item = slot.item;
                    item.data = slot.item.data;
                    item.amount++;
                    slot.item.amount--;
                }
                else
                {
                    return;
                }

                CargoManager.instance.RemoveCargo(slot.item.itemName);
                Earn(Shop.instance.islandPrices.FindItemPriceByName(slot.item.itemName));
                slot.item.amount--;
            }
        }
    }

    public void BuyItem()
    {
        GameObject g = EventSystem.current.currentSelectedGameObject;
        if (g != null && g.GetComponent<Slot>() != null)
        {
            Slot shopSlot = g.GetComponent<Slot>();

            if (shopSlot.IsEmpty() || inventorySlots.Contains(shopSlot) == true) return;
            else
            {
                if(EnoughMoney(Shop.instance.islandPrices.FindItemPriceByName(shopSlot.item.itemName)))
                {
                    if(HasItem(shopSlot.item.itemName, true))
                    {
                        Debug.Log("A");
                        GetItem(shopSlot.item.itemName).amount++;
                        shopSlot.item.amount--;
                    }
                    else if(AnySlotAvailable())
                    {
                        Slot invSlot = GetFirstAvailableSlot();
                        Debug.Log(invSlot.item.data + " INVDATA");
                        invSlot.item.data = shopSlot.item.data;
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

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                vecs[i * j].y = i * -(slotTemplate.GetComponent<RectTransform>().rect.height + yOffset) + yMargin;
                vecs[i * j].x = j * (slotTemplate.GetComponent<RectTransform>().rect.width + xOffset) + xMargin;
                GameObject go = Instantiate(slotTemplate, vecs[i*j], Quaternion.identity);
                inventorySlots.Add(go.GetComponent<Slot>());
                go.transform.SetParent(this.transform, false);
            }
        }
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
            if (checkMaxStack && s.locked) return false;

            if (s.item.itemName == item) return true;
        }

        return false;
    }

    public Item GetItem(ItemType item)
    {
        foreach (Slot s in inventorySlots)
        {
            if (s.item.itemName == item) return s.item;
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
}
