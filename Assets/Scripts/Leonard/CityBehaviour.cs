using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityBehaviour : MonoBehaviour
{
    [SerializeField] private IslandPrices myPrices;

    public void SetIslandPrices() => Shop.instance.islandPrices = myPrices;
    public void OpenShop() { } //TODO : Open the shop
}