using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    [Header("Refs")]
    public ItemData item;
    public Image visual;
    public TextMeshProUGUI stackText;

    [Header("Data")]
    public bool unlimitedStack = false;
    public int amount;
    int lastAmount;
    public ItemType itemName;

    public bool locked { get; private set; }

    [HideInInspector] public UnityEvent OnStackValueChange;

    private void Start()
    {
        UpdateStackText();
        OnStackValueChange.AddListener(UpdateStackText);
        OnStackValueChange.AddListener(StackOverflow);
        OnStackValueChange.AddListener(ClearSlot);
    }

    private void Update()
    {
        if (item != null && lastAmount != amount)
        {
            OnStackValueChange.Invoke();
            lastAmount = amount;
        }
    }
   

    public bool IsFull()
    {
        return amount >= item.maxStack;
    }

    public bool IsEmpty()
    {
        return amount <= 0;
    }

    public void ClearSlot()
    {
        if (amount <= 0)
        {
            item = null;
            locked = false;
        }
    }

    public void StackOverflow()
    {
        if (IsFull() && unlimitedStack == false)
        {
            locked = true;
        }
        else locked = false;
    }

    public void UpdateStackText()
    {
        stackText.text = amount > 0 ? amount.ToString() : "";
    }


}
