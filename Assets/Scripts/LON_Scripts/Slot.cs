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

    [HideInInspector] public UnityEvent OnStackValueChange;

    private void Start()
    {
        UpdateStackText();
        OnStackValueChange.AddListener(UpdateStackText);
    }

    private void Update()
    {
        if(lastAmount != amount)
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

    public void UpdateStackText()
    {
        stackText.text = amount > 0 ? amount.ToString() : "";
    }


}
