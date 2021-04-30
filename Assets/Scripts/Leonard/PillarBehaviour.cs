using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarBehaviour : MonoBehaviour
{
    [SerializeField] PlaneManager planeIntegrityRef;
    [SerializeField] private float planedamagePercentage = 0.15f;
    [SerializeField] private float cargodamagePercentage = 0.20f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlaneManager>())
        {
            planeIntegrityRef = collision.gameObject.GetComponent<PlaneManager>();
            planeIntegrityRef.PillarDamage(planedamagePercentage, cargodamagePercentage);
            planeIntegrityRef._planeMovement.KillSpeed();
            
            AudioManager.instance.PlaySFX(PlaneManager.instance.planeImpact);
            
            collision.gameObject.GetComponent<Rigidbody>().velocity =
                (planeIntegrityRef.transform.position - transform.position) *3f;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlaneManager>() && planeIntegrityRef)
        {
            planeIntegrityRef.isBouncing = true;
            planeIntegrityRef._planeMovement.ResetMovement();
            planeIntegrityRef = null;
        }
    }
}