using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour
{
    [Header("Refs")]
    public Item item;
    public Image visual;
    public TextMeshProUGUI stackText;
    public Image overlapFrame;
    public Image initialFrame;

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
        if (EventSystem.current.currentSelectedGameObject == this.gameObject) overlapFrame.enabled = true;
        else overlapFrame.enabled = false;

        if (item != null && lastAmount != item.amount)
        {
            visual.sprite = item.data.sprite != null ? item.data.sprite : null;
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
        if (IsEmpty())
        {
            visual.sprite = null;
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
