using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractCloudBehaviour : MonoBehaviour
{
    [SerializeField] protected PlaneManager planeManagerRef;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlaneManager>()) 
            planeManagerRef = other.GetComponent<PlaneManager>();
    }
}