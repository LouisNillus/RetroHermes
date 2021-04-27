using System;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class CargoManager : MonoBehaviour
{
    [ShowInInspector] Dictionary<AbstractCargo, int> cargoHold = new Dictionary<AbstractCargo, int>();

    private void Update() => ApplyAllEffects();

    // called when the player buys cargo of a certain type
    public void AddCargo(AbstractCargo cargoType)
    {
        if (cargoHold.ContainsKey(cargoType)) cargoHold[cargoType]++; // increment the number of items in the hold
        else cargoHold.Add(cargoType, 1); // create a new item
    }

    // called when the player sells cargo of a certain type
    public void RemoveCargo(AbstractCargo cargoType)
    {
        --cargoHold[cargoType]; // decrement the number of items in the hold
        if (cargoHold[cargoType] == 0) cargoHold.Remove(cargoType); // remove items if it was the last one
    }
    
    void ApplyAllEffects()
    {
        foreach (KeyValuePair<AbstractCargo, int> cargoItem in cargoHold)
            cargoItem.Key.ApplyEffect();
    }
}