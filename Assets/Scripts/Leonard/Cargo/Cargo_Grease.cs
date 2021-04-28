using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo_Grease : AbstractCargo
{
    public override void ApplyEffect() => Debug.Log("Grease");

    public override void StormDamage(float stormDamagePercentage)
    {
        stormDamagePercentage *= 2;
        base.StormDamage(stormDamagePercentage);
    }
}