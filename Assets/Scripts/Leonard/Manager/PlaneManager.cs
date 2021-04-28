using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    [SerializeField] public PlaneBehaviour_Movement _planeMovement;
    [SerializeField]  PlaneBehaviour_Integrity _planeIntegrity;
    [SerializeField] PlaneBehaviour_Fuel _planeFuel;
    [SerializeField] PlaneBehaviour_Landing _planeLanding;

    [SerializeField] CargoManager _cargoManager;

    private static PlaneManager _instance;

    public static PlaneManager instance
    {
        get { return _instance; }
        private set { _instance = value; }
    }

    private void Awake() => instance = this;

    public void StormDamage(float planeDamage, float cargoDamage)
    {
        _planeIntegrity.TakeDamage(_planeIntegrity.baseIntegrity * planeDamage);
        // TODO : _planeMovement.KillSpeed();

        foreach (var cargo in _cargoManager.cargoHold)
            cargo.StormDamage(cargoDamage);
    }

    public void PillarDamage(float planeDamage, float cargoDamage)
    {
        _planeIntegrity.TakeDamage(_planeIntegrity.baseIntegrity * planeDamage);

        foreach (var cargo in _cargoManager.cargoHold)
            cargo.PillarDamage(cargoDamage);
    }
}