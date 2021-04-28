using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class CargoManager : MonoBehaviour
{
    [ShowInInspector] Dictionary<ItemType, int> cargoTypes = new Dictionary<ItemType, int>();
    [ShowInInspector] public List<AbstractCargo> cargoHold { get; private set; }

    public static CargoManager instance;

    private void Awake()
    {
        instance = this;
        cargoHold = new List<AbstractCargo>();
    }

    void Update()
    {
        foreach (var cargoItem in cargoHold)
            cargoItem.ApplyEffect();

        //if (Input.GetKeyDown(KeyCode.A)) AddCargo(ItemType.Bottles);
        //if (Input.GetKeyDown(KeyCode.B)) AddCargo(ItemType.Bananas);
        //if (Input.GetKeyDown(KeyCode.C)) AddCargo(ItemType.Crocodile);
        //if (Input.GetKeyDown(KeyCode.D)) RemoveCargo(ItemType.Bottles);
        //if (Input.GetKeyDown(KeyCode.E)) RemoveCargo(ItemType.Bananas);
        //if (Input.GetKeyDown(KeyCode.F)) RemoveCargo(ItemType.Crocodile);
    }

    // called when the player buys cargo of a certain type
    public void AddCargo(ItemType cargoType, int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            if (!cargoTypes.ContainsKey(cargoType)) cargoTypes.Add(cargoType, 0); // create a new item

            switch (cargoType)
            {
                case ItemType.Bananas:
                    cargoHold.Add(new Cargo_Banana());
                    break;
                case ItemType.Bottles:
                    cargoHold.Add(new Cargo_Bottles());
                    break;
                case ItemType.Crocodile:
                    cargoHold.Add(new Cargo_Crocodile());
                    break;
                case ItemType.Eggs:
                    cargoHold.Add(new Cargo_Eggs());
                    break;
                case ItemType.Explosive:
                    cargoHold.Add(new Cargo_Explosive());
                    break;
                case ItemType.Grease:
                    cargoHold.Add(new Cargo_Grease());
                    break;
                case ItemType.Magnets:
                    cargoHold.Add(new Cargo_Magnets());
                    break;
                case ItemType.Ore:
                    cargoHold.Add(new Cargo_Ore());
                    break;
                case ItemType.Paintings:
                    cargoHold.Add(new Cargo_Painting());
                    break;
                default:
                    Debug.Log("Couldn't add that type of cargo");
                    break;
            }

            cargoTypes[cargoType]++; // increment the amount of cargo of this type
        }

    }

    // called when the player sells cargo of a certain type
    public void RemoveCargo(ItemType cargoType, int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            // decrement cargo
            if (--cargoTypes[cargoType] != 0)
                cargoTypes.Remove(cargoType);

            switch (cargoType)
            {
                case ItemType.Bananas:
                    cargoHold.Remove(cargoHold.Find(x => x.GetType() == typeof(Cargo_Banana)));
                    break;
                case ItemType.Bottles:
                    cargoHold.Remove(cargoHold.Find(x => x.GetType() == typeof(Cargo_Bottles)));
                    break;
                case ItemType.Crocodile:
                    cargoHold.Remove(cargoHold.Find(x => x.GetType() == typeof(Cargo_Crocodile)));
                    break;
                case ItemType.Eggs:
                    cargoHold.Remove(cargoHold.Find(x => x.GetType() == typeof(Cargo_Eggs)));
                    break;
                case ItemType.Explosive:
                    cargoHold.Remove(cargoHold.Find(x => x.GetType() == typeof(Cargo_Explosive)));
                    break;
                case ItemType.Grease:
                    cargoHold.Remove(cargoHold.Find(x => x.GetType() == typeof(Cargo_Grease)));
                    break;
                case ItemType.Magnets:
                    cargoHold.Remove(cargoHold.Find(x => x.GetType() == typeof(Cargo_Magnets)));
                    break;
                case ItemType.Ore:
                    cargoHold.Remove(cargoHold.Find(x => x.GetType() == typeof(Cargo_Ore)));
                    break;
                case ItemType.Paintings:
                    cargoHold.Remove(cargoHold.Find(x => x.GetType() == typeof(Cargo_Painting)));
                    break;
                default:
                    Debug.Log("Couldn't destroy that type of cargo");
                    break;
            }
        }
    }
}