using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class ShopItem : MonoBehaviour
{
    public Slot slot;
    bool token = true;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI islandPrice;

    // Start is called before the first frame update
    void Start()
    {
        OnSlotItemChange.AddListener(UpdateInfos);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlot();
        ZeroStockKill();
    }

    public void UpdateInfos()
    {
        itemName.text = slot.item.itemName.ToString();
        islandPrice.text = Shop.instance.islandPrices.FindItemPriceByName(slot.item.itemName).ToString() + "$/unit";
        slot.visual.sprite = slot.item.data.sprite != null ? slot.item.data.sprite : null;
    }

    public void UpdateSlot()
    {
        if (slot.item != null && token)
        {
            OnSlotItemChange.Invoke();
        }
        else if (slot.item == null) token = true;
    }

    public void ZeroStockKill()
    {
        if(slot.item.amount <= 0)
        {
            Shop.instance.allItems.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    [HideInInspector] public UnityEvent OnSlotItemChange;
}
