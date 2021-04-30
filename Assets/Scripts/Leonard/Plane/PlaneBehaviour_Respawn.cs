using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneBehaviour_Respawn : MonoBehaviour
{
    public Vector3 respawnLocation;

    private void Awake()
    {
        respawnLocation = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CityBehaviour>())
        {
            respawnLocation = other.GetComponent<CityBehaviour>().transform.position;
        }
    }
}