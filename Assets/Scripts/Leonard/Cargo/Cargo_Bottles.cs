using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo_Bottles : AbstractCargo
{
    public override void ApplyEffect() => Debug.Log("Glass");

    public override void StormDamage(float stormDamagePercentage)
    {
        stormDamagePercentage *= 2;
        base.StormDamage(stormDamagePercentage);
    }

    public override void PillarDamage(float pillarDamagePercentage)
    {
        pillarDamagePercentage *= 2;
        base.PillarDamage(pillarDamagePercentage);
    }
}