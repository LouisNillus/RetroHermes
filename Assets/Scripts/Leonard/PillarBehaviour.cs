using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarBehaviour : MonoBehaviour
{
    [SerializeField] PlaneManager planeIntegrityRef;
    [SerializeField] private float planedamagePercentage = 0.15f;
    [SerializeField] private float cargodamagePercentage = 0.20f;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlaneManager>())
        {
            planeIntegrityRef = other.GetComponent<PlaneManager>();
            planeIntegrityRef.PillarDamage(planedamagePercentage, cargodamagePercentage);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlaneManager>() && planeIntegrityRef) planeIntegrityRef = null;
    }
}