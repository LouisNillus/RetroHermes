using System.Linq.Expressions;
using Sirenix.OdinInspector;
using UnityEngine;

public class AbstractCargo
{
    [ReadOnly] [SerializeField] protected float baseIntegrity = 100f;
    [ReadOnly] [SerializeField] public float currentIntegrity;
    public bool cargoDestroyed;
    
    public AbstractCargo() =>  currentIntegrity = baseIntegrity;

    public virtual void ApplyEffect()
    {
    }

    public virtual void SpecifyDamage(float damagePercentage, DamageType damageType)
    {
        currentIntegrity -= currentIntegrity > 0 ? baseIntegrity * damagePercentage : 0;
        if (currentIntegrity <= 0) cargoDestroyed = true;
    }
}