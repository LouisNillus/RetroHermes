using UnityEngine;

public class AbstractCargo
{
    private float baseIntegrity = 100f;
    private float currentIntegrity;

    public virtual void ApplyEffect() { }

    public virtual void StormDamage(float stormDamagePercentage)
    {
        currentIntegrity -= currentIntegrity > 0 ? baseIntegrity * stormDamagePercentage : 0;
        //integrityText.text = currentIntegrity.ToString();
    }

   // public virtual void PillarDamage();
}