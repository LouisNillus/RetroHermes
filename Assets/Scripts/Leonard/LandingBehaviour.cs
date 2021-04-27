using System;
using UnityEngine;

public class LandingBehaviour : MonoBehaviour
{
    private CityBehaviour currentCity;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y) && currentCity) LandingSequence();
    }

    private void OnTriggerEnter(Collider other)
    {
        currentCity = other?.GetComponent<CityBehaviour>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentCity) currentCity = null;
    }

    private void LandingSequence()
    {
        Debug.Log("Landed in City");
        currentCity.SetIslandPrices();
        currentCity.OpenShop();
    }
}