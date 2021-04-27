using System;
using UnityEngine;

public class LandingBehaviour : MonoBehaviour
{
    private bool isOverCity;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y) && isOverCity) LandingSequence();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CityBehaviour>()) isOverCity = true;
    }

    private void OnTriggerExit(Collider other)
    {        
        if (other.GetComponent<CityBehaviour>()) isOverCity = false;
    }

    private void LandingSequence()
    {
        Debug.Log("Landed in City");
    }
}
