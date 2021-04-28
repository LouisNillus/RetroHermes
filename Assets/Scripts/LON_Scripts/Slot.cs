using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    [Header("Refs")]
    public Item item;
    public Image visual;
    public TextMeshProUGUI stackText;

    [Header("Data")]
    public bool unlimitedStack = false;
    int lastAmount;

    [ShowInInspector] public bool locked { get; private set; }

    [HideInInspector] public UnityEvent OnStackValueChange;

    private void Start()
    {
        UpdateStackText();
        OnStackValueChange.AddListener(UpdateStackText);
        OnStackValueChange.AddListener(StackOverflow);
    }

    private void Update()
    {
        if (item != null && lastAmount != item.amount)
        {
            OnStackValueChange.Invoke();
            lastAmount = item.amount;
            ClearSlot();
        }
    }

    public bool IsFull()
    {
        return item.amount >= item.data.maxStack;
    }

    public bool IsEmpty()
    {
        return item.amount <= 0;
    }

    public void ClearSlot()
    {
        if (item.amount <= 0)
        {
            item = new Item();
            locked = false;          
        }
    }

    public void StackOverflow()
    {
        if (item != null && IsFull() && item.unlimitedStack == false)
        {
            locked = true;
        }
        else locked = false;
    }

    public void UpdateStackText()
    {
        if(item != null)
        stackText.text = item.amount > 0 ? item.amount.ToString() : "";
    }


}
