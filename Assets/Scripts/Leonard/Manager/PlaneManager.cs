using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlaneManager : MonoBehaviour
{
    public PlaneBehaviour_Movement _planeMovement;
    public PlaneBehaviour_Integrity _planeIntegrity;
    public PlaneBehaviour_Fuel _planeFuel;
    public PlaneBehaviour_Landing _planeLanding;
    public PlaneBehaviour_Respawn _planeRespawn;
    public PlaneBehavior_Explosion _planeExplosion;
    [HideInInspector] public bool landed, openedMap;
    [SerializeField] CargoManager _cargoManager;

    private static PlaneManager _instance;

    public static PlaneManager instance
    {
        get { return _instance; }
        private set { _instance = value; }
    }
    
   [HideInInspector]public bool isInCloud;
   [HideInInspector]public bool demag_Compass;
   [HideInInspector]public bool isBouncing;
   [HideInInspector]public bool mustRespawn;
   [HideInInspector]public float respawnTime;
   [HideInInspector]public float bounceTime;

    public AudioClip explosion;
    public AudioClip planeFying;
    public AudioClip planeLanding;
    public AudioClip planeImpact;

    private void Awake() => instance = this;

    public void LandingSequence()
    {
        landed = true;
        //_planeLanding.currentCity.SetIslandPrices();
        _planeLanding.currentCity.OpenShop();
        _planeFuel.Refuel();
        _planeIntegrity.RegenPlane();
        _planeMovement.KillSpeed();
        AudioManager.instance.PlaySFX(planeLanding);
    }

    private void Respawn()
    {
        _planeFuel.Refuel();
        _planeIntegrity.RegenPlane();
        _planeMovement.ResetMovement();
        transform.position = _planeRespawn.respawnLocation;

        respawnTime = 0f;
        mustRespawn = false;
    }

    private void FixedUpdate()
    {
        if (isBouncing)
        {
            bounceTime += Time.deltaTime;
            if (bounceTime >= .5f)
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                bounceTime = 0f;
                isBouncing = false;
            }
        }
    }

    private void Update()
    {
        if (!landed && _planeFuel.currentFuel > 0) // && !openedMap)
        {
            _planeFuel.ConsumeFuel();
            _planeMovement.MovementLogic();
        }

        if (mustRespawn)
        {
            respawnTime += Time.deltaTime;
            if (respawnTime >= 2)
            {
                Respawn();
            }
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