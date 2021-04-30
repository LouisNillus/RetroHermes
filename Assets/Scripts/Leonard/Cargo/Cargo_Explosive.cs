using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Cargo_Explosive : AbstractCargo
{
    private float damageFrequency = 1f;
    private float elapsedtime = 1f;

    private float damagePercentage = 0.05f;

    private bool explosion = false;

    public override void ApplyEffect()
    {
        if (!PlaneManager.instance._planeMovement.isFullSpeed)
        {
            // every 1 second
            if (elapsedtime >= damageFrequency)
            {
                elapsedtime = 0;
                SpecifyDamage(damagePercentage, DamageType.Cloud);
            }

            elapsedtime += Time.deltaTime;

            if (currentIntegrity <= (baseIntegrity * 0.5f) && !explosion)
            {
                explosion = true;
                PlaneManager.instance._planeExplosion.Explode();
                PlaneManager.instance.ExplosiveDamage();
            }
        }
    }
}