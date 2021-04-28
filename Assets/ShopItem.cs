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
    }

    public void UpdateInfos()
    {
        itemName.text = slot.itemName.ToString();
        islandPrice.text = Shop.instance.islandPrices.FindItemPriceByName(slot.itemName).ToString() + "$/unit";
        slot.visual.sprite = slot.item.sprite != null ? slot.item.sprite : null;
    }

    public void UpdateSlot()
    {
        if (slot.item != null && token)
        {
            OnSlotItemChange.Invoke();
        }
        else if (slot.item == null) token = true;
    }

    [HideInInspector] public UnityEvent OnSlotItemChange;
}
