using Sirenix.OdinInspector;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CargoManager : MonoBehaviour
{
    [ShowInInspector] Dictionary<ItemType, int> cargoTypes = new Dictionary<ItemType, int>();
    private List<AbstractCargo> cargoHold = new List<AbstractCargo>();

    void Update()
    {
        foreach (var cargoItem in cargoHold)
            cargoItem.ApplyEffect();

        if (Input.GetKeyDown(KeyCode.B)) AddCargo(ItemType.Bananas);
        if (Input.GetKeyDown(KeyCode.C)) AddCargo(ItemType.Crocodile);
        if (Input.GetKeyDown(KeyCode.D)) RemoveCargo(ItemType.Crocodile);
        if (Input.GetKeyDown(KeyCode.E)) RemoveCargo(ItemType.Crocodile);
    }

    // called when the player buys cargo of a certain type
    public void AddCargo(ItemType cargoType)
    {
        if (!cargoTypes.ContainsKey(cargoType))
        {
            cargoTypes.Add(cargoType, 0); // create a new item with 

            switch (cargoType)
            {
                case ItemType.Bananas:
                    cargoHold.Add(gameObject.AddComponent<Cargo_Banana>());
                    break;
                case ItemType.Bottles:
                    cargoHold.Add(gameObject.AddComponent<Cargo_Bottles>());
                    break;
                case ItemType.Crocodile:
                    cargoHold.Add(gameObject.AddComponent<Cargo_Crocodile>());
                    break;
                case ItemType.Eggs:
                    cargoHold.Add(gameObject.AddComponent<Cargo_Eggs>());
                    break;
                case ItemType.Explosive:
                    cargoHold.Add(gameObject.AddComponent<Cargo_Explosive>());
                    break;
                case ItemType.Grease:
                    cargoHold.Add(gameObject.AddComponent<Cargo_Grease>());
                    break;
                case ItemType.Magnets:
                    cargoHold.Add(gameObject.AddComponent<Cargo_Magnets>());
                    break;
                case ItemType.Ore:
                    cargoHold.Add(gameObject.AddComponent<Cargo_Ore>());
                    break;
                case ItemType.Paintings:
                    cargoHold.Add(gameObject.AddComponent<Cargo_Painting>());
                    break;
                default:
                    Debug.Log("Couldn't add that type of cargo");
                    break;
            }
        }

        cargoTypes[cargoType]++; // increment the number of items in the hold
    }

    // called when the player sells cargo of a certain type
    public void RemoveCargo(ItemType cargoType)
    {
        --cargoTypes[cargoType]; // decrement the number of items in the hold

        if (cargoTypes[cargoType] == 0)
        {
            cargoTypes.Remove(cargoType); // remove items if it was the last one

            switch (cargoType)
            {
                case ItemType.Bananas:
                    cargoHold.Remove(gameObject.GetComponent<Cargo_Banana>());
                    Destroy(gameObject.GetComponent<Cargo_Banana>());
                    break;
                case ItemType.Bottles:
                    cargoHold.Remove(gameObject.GetComponent<Cargo_Bottles>());
                    Destroy(gameObject.GetComponent<Cargo_Bottles>());
                    break;
                case ItemType.Crocodile:
                    cargoHold.Remove(gameObject.GetComponent<Cargo_Crocodile>());
                    Destroy(gameObject.GetComponent<Cargo_Crocodile>());
                    break;
                case ItemType.Eggs:
                    cargoHold.Remove(gameObject.GetComponent<Cargo_Eggs>());
                    Destroy(gameObject.GetComponent<Cargo_Eggs>());
                    break;
                case ItemType.Explosive:
                    cargoHold.Remove(gameObject.GetComponent<Cargo_Explosive>());
                    Destroy(gameObject.GetComponent<Cargo_Explosive>());
                    break;
                case ItemType.Grease:
                    cargoHold.Remove(gameObject.GetComponent<Cargo_Grease>());
                    Destroy(gameObject.GetComponent<Cargo_Grease>());
                    break;
                case ItemType.Magnets:
                    cargoHold.Remove(gameObject.GetComponent<Cargo_Magnets>());
                    Destroy(gameObject.GetComponent<Cargo_Magnets>());
                    break;
                case ItemType.Ore:
                    cargoHold.Remove(gameObject.GetComponent<Cargo_Ore>());
                    Destroy(gameObject.GetComponent<Cargo_Ore>());
                    break;
                case ItemType.Paintings:
                    cargoHold.Remove(gameObject.GetComponent<Cargo_Painting>());
                    Destroy(gameObject.GetComponent<Cargo_Painting>());
                    break;
                default:
                    Debug.Log("Couldn't destroy that type of cargo");
                    break;
            }
        }
    }
}