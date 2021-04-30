using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneBehaviour_Respawn : MonoBehaviour
{
    public Vector3 respawnLocation;
    public Vector3 respawnRotation;

    private void Awake()
    {
        respawnLocation = transform.position;
        respawnRotation = transform.eulerAngles;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CityBehaviour>())
        {
            respawnLocation = other.GetComponent<CityBehaviour>().transform.position;
            respawnRotation = other.GetComponent<CityBehaviour>().transform.eulerAngles;
        }
    }
}