using System;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlaneBehaviour_Landing : AbstractPlaneBehaviour
{
    [Header("Debugging")] [ReadOnly] [SerializeField]
    public CityBehaviour currentCity;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y) && currentCity)
            PlaneManager.instance.LandingSequence();
    }

    private void OnTriggerStay(Collider other) => currentCity = other?.GetComponent<CityBehaviour>();

    private void OnTriggerExit(Collider other)
    {
        if (currentCity) currentCity = null;
    }
}