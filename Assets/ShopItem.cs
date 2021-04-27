using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class ShopItem : MonoBehaviour
{
    public Slot slot;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI islandPrice;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateInfos()
    {
        //if()
    }

    [HideInInspector] public UnityEvent OnSlotItemChange;
}
