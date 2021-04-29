using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlaneManager : MonoBehaviour
{
    [SerializeField] public PlaneBehaviour_Movement _planeMovement;
    [SerializeField] PlaneBehaviour_Integrity _planeIntegrity;
    [SerializeField] PlaneBehaviour_Fuel _planeFuel;
    [SerializeField] PlaneBehaviour_Landing _planeLanding;
    private bool landed;
    [SerializeField] CargoManager _cargoManager;

    private static PlaneManager _instance;

    public static PlaneManager instance
    {
        get { return _instance; }
        private set { _instance = value; }
    }

    public bool isInCloud;
    public CompassBehaviour compass;

    private void Awake() => instance = this;

    public void LandingSequence()
    {
        landed = true;
        //_planeLanding.currentCity.SetIslandPrices();
        _planeLanding.currentCity.OpenShop();
        _planeFuel.Refuel();
        _planeMovement.KillSpeed();
    }

    private void Update()
    {
        if (!landed)
        {
            _planeFuel.ConsumeFuel();
            _planeMovement.MovementLogic();
        }
    }

    public void TakeOff()
    {
        landed = false;
        _planeMovement.Takeoff();
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