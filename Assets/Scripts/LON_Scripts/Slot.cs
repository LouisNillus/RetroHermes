using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public ItemData item;
    public Image visual;
    int lastAmount;
    public int amount;
    public TextMeshProUGUI stackText;
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
        if (IsFull())
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
