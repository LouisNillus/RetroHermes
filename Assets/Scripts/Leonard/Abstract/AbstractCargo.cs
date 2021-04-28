using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCargo : MonoBehaviour
{
    private float baseIntegrity = 100f;
    private float currentIntegrity;

    public abstract void ApplyEffect();

    public virtual void StormDamage(float stormDamagePercentage)
    {
        currentIntegrity -= currentIntegrity > 0 ? baseIntegrity * stormDamagePercentage : 0;
        //integrityText.text = currentIntegrity.ToString();
    }
}