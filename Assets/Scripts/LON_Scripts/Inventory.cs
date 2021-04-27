using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public List<Slot> slots = new List<Slot>();
    public GameObject slotTemplate;

    [Range(0,200)]
    public int yOffset; //In pixels
    [Range(0,200)]
    public int xOffset; //In pixels
    [Range(-1000,1000)]
    public int yMargin; //In pixels
    [Range(-1000,1000)]
    public int xMargin; //In pixels

    public int rows;
    public int columns;

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
            Slot s = g.GetComponent<Slot>();

            if (s.IsEmpty() || slots.Contains(s) == false) return;
            else
            {
                money += Shop.instance.islandPrices.FindItemPriceByName(s.item.itemName);
                s.amount--;
            }
        }
    }

    public void BuyItem()
    {
        GameObject g = EventSystem.current.currentSelectedGameObject;
        if (g != null && g.GetComponent<Slot>() != null)
        {
            Slot s = g.GetComponent<Slot>();

            if (s.IsEmpty() || slots.Contains(s) == true) return;
            else
            {
                if(HasItem(s.item))
                {
                    GetItem(s.item).amount++;
                    s.amount--;
                }
                else if(AnySlotAvailable())
                {
                    GetFirstAvailableSlot().item = s.item;
                    GetFirstAvailableSlot().amount++;
                    s.amount--;
                }
                else
                {
                    return;
                }

                Pay(Shop.instance.islandPrices.FindItemPriceByName(s.item.itemName));
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
                slots.Add(go.GetComponent<Slot>());
                go.transform.SetParent(this.transform, false);
            }
        }
    }

    public bool AnySlotAvailable()
    {
        foreach(Slot s in slots)
        {
            if (s.item == null) return true;
        }

        return false;
    }

    public Slot GetFirstAvailableSlot()
    {
        foreach (Slot s in slots)
        {
            if (s.item == null) return s;
        }

        return null;
    }

    public bool HasItem(ItemData item)
    {
        foreach (Slot s in slots)
        {
            if (s.item == item) return true;
        }

        return false;
    }

    public Slot GetItem(ItemData item)
    {
        foreach (Slot s in slots)
        {
            if (s.item == item) return s;
        }

        return null;
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
