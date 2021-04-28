using System.Linq.Expressions;
using Sirenix.OdinInspector;
using UnityEngine;

public class AbstractCargo
{
    [ReadOnly] [SerializeField] float baseIntegrity = 100f;
    [ReadOnly] [SerializeField] float currentIntegrity;
    public bool cargoDestroyed;
    
    public AbstractCargo() =>  currentIntegrity = baseIntegrity;

    public virtual void ApplyEffect()
    {
    }

    public virtual void StormDamage(float stormDamagePercentage)
    {
        currentIntegrity -= currentIntegrity > 0 ? baseIntegrity * stormDamagePercentage : 0;
        if (currentIntegrity <= 0) cargoDestroyed = true;
    }

    public virtual void PillarDamage(float pillarDamagePercentage)
    {
        currentIntegrity -= currentIntegrity > 0 ? baseIntegrity * pillarDamagePercentage : 0;
        if (currentIntegrity <= 0) cargoDestroyed = true;
    }
}