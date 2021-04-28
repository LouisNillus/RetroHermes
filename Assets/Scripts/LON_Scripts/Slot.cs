using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        if (item != null && lastAmount != item.amount)
        {
            OnStackValueChange.Invoke();
            lastAmount = item.amount;
        }
    }

    public bool IsFull()
    {
        Debug.Log((item == null).ToString() + " " + (item.data == null).ToString());
        Debug.Log(item.data);
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
            item = null;
            locked = false;
        }
    }

    public void StackOverflow()
    {
        if (IsFull() && item.unlimitedStack == false)
        {
            locked = true;
        }
        else locked = false;
    }

    public void UpdateStackText()
    {
        stackText.text = item.amount > 0 ? item.amount.ToString() : "";
    }


}
