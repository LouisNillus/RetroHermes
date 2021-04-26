using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop instance;
    public IslandPrices islandPrices;

    private void Awake()
    {
        instance = this;
    }
}
