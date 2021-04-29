using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class CargoManager : MonoBehaviour
{
    [ShowInInspector] public List<AbstractCargo> cargoHold { get; private set; }
    private bool collect;

    public static CargoManager instance;

    private void Awake()
    {
        instance = this;
        cargoHold = new List<AbstractCargo>();
    }

    void Update()
    {
        for (int i = 0; i < cargoHold.Count; i++)
        {
            cargoHold[i].ApplyEffect();
            if (cargoHold[i].cargoDestroyed)
            {
                PlaneManager.instance.compass.TrackPlayerNorth();
                cargoHold.RemoveAt(i);
                i--;
            }
        }
    }

    // called when the player buys cargo of a certain type
    public void AddCargo(ItemType cargoType, int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
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
        }
    }

    // called when the player sells cargo of a certain type
    public void RemoveCargo(ItemType cargoType)
    {
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

        // reset movement just in case
        PlaneManager.instance._planeMovement.ResetMovement();
    }
}