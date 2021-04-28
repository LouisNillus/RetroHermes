using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    [SerializeField] PlaneBehaviour_Integrity _planeIntegrity;
    [SerializeField] private CargoManager _cargoManager;

    public void StormDamage(float planeDamage, float cargoDamage)
    {
        _planeIntegrity.TakeDamage(_planeIntegrity.baseIntegrity * planeDamage);
        
        foreach (var cargo in _cargoManager.cargoHold)
            cargo.StormDamage(cargoDamage);
    }
}
