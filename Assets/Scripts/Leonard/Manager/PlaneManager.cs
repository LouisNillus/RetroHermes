using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlaneManager : MonoBehaviour
{
    [SerializeField] public PlaneBehaviour_Movement _planeMovement;
    [SerializeField] public PlaneBehaviour_Integrity _planeIntegrity;
    [SerializeField] PlaneBehaviour_Fuel _planeFuel;
    [SerializeField] PlaneBehaviour_Landing _planeLanding;
    [HideInInspector] public bool landed, openedMap;
    [SerializeField] CargoManager _cargoManager;

    private static PlaneManager _instance;

    public static PlaneManager instance
    {
        get { return _instance; }
        private set { _instance = value; }
    }

    public bool isInCloud;
    public bool demag_Compass;

    private void Awake() => instance = this;

    public void LandingSequence()
    {
        landed = true;
        //_planeLanding.currentCity.SetIslandPrices();
        _planeLanding.currentCity.OpenShop();
        _planeFuel.Refuel();
        _planeIntegrity.RegenPlane();
        _planeMovement.KillSpeed();
    }

    private void Update()
    {
        if (!landed && _planeFuel.currentFuel > 0) // && !openedMap)
        {
            _planeFuel.ConsumeFuel();
            _planeMovement.MovementLogic();
        }
    }

    public void TakeOff()
    {
        landed = false;
        _planeMovement.ResetMovement();
    }

    public void StormDamage(float planeDamage, float cargoDamage)
    {
        _planeIntegrity.TakeDamage(_planeIntegrity.baseIntegrity * planeDamage);

        foreach (var cargo in _cargoManager.cargoHold)
            cargo.SpecifyDamage(cargoDamage, DamageType.Storm);
    }

    public void PillarDamage(float planeDamage, float cargoDamage)
    {
        _planeIntegrity.TakeDamage(_planeIntegrity.baseIntegrity * planeDamage);

        foreach (var cargo in _cargoManager.cargoHold)
            cargo.SpecifyDamage(cargoDamage, DamageType.Pillar);
    }

    public void ExplosiveDamage()
    {
        // TODO : explosion FX call
        _planeIntegrity.currentIntegrity /= 2;
    }
}