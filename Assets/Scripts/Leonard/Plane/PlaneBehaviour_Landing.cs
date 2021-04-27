using System;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlaneBehaviour_Landing : AbstractPlaneBehaviour
{
    [Header("Debugging")] [ReadOnly] [SerializeField]
    private CityBehaviour currentCity;

    private PlaneBehaviour_Fuel fuelBehaviour;

    private void Awake() => fuelBehaviour = GetComponent<PlaneBehaviour_Fuel>();

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
        fuelBehaviour.Refuel();
    }
}