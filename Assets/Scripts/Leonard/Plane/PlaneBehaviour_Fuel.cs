using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class PlaneBehaviour_Fuel : AbstractPlaneBehaviour
{
    [SerializeField] private float baseFuel;

    [SerializeField] [Tooltip("How much fuel the plane loses per second.")]
    private float depletionRate;

    [SerializeField] Slider fuelSlider;

    [Space][Header("Debugging")]
    [ReadOnly] [SerializeField] private float currentFuel;
    private void Awake()
    {
        currentFuel = baseFuel;
        fuelSlider.value = (float) (currentFuel / baseFuel);
    }

    private void Update()
    {
        currentFuel -= currentFuel > 0 ? Time.deltaTime * depletionRate : 0;
        fuelSlider.value = (currentFuel / baseFuel);
        
        if (currentFuel <= 0) Debug.Log("You Crashed.");
    }

    public void Refuel() => currentFuel = baseFuel; // full tank
    public void Refuel(int fuelAmount) => currentFuel += fuelAmount; // specified amount
}