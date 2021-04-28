using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityBehaviour : MonoBehaviour
{
    [SerializeField] private Island island;

    //public void SetIslandPrices() => Shop.instance.islandPrices = myPrices;
    public void OpenShop()
    {
        Shop.instance.islandPanel.SetActive(true);
        island.ShopSubscribe();
    }
}