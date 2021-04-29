using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo_Grease : AbstractCargo
{
    public override void ApplyEffect() 
    {
    }

    public override void SpecifyDamage(float damagePercentage, DamageType damageType)
    {
        if (damageType == DamageType.Storm) damagePercentage *= 2;
        base.SpecifyDamage(damagePercentage, damageType);
    }
}