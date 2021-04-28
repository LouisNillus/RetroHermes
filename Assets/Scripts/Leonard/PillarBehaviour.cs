using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarBehaviour : MonoBehaviour
{
    [SerializeField] PlaneBehaviour_Integrity planeIntegrity;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlaneBehaviour_Integrity>())
        {
            planeIntegrity = other.GetComponent<PlaneBehaviour_Integrity>();
            planeIntegrity.TakeDamage();
            
            // TODO : destroy pillar animation (?)
            
            Destroy(this);
        }
    }
}