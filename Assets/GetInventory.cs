using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetInventory : MonoBehaviour
{
    public List<Image> slots = new List<Image>();

    private void OnEnable()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].sprite = Inventory.instance.inventorySlots[i].visual.sprite;

            if(Inventory.instance.inventorySlots[i].item.amount == 0)
            {
                slots[i].gameObject.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
            else
            {
                slots[i].gameObject.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = Inventory.instance.inventorySlots[i].item.amount.ToString();
            }
        }        
    }
}
