using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneBehaviour_Respawn : MonoBehaviour
{
    public CityBehaviour lastCity;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CityBehaviour>()) lastCity = other.GetComponent<CityBehaviour>();
    }
}
