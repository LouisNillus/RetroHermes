using UnityEngine;

public class Cargo_Eggs : AbstractCargo
{
    private float damageFrequency = 3f;
    private float elapsedtime = 0f;

    private float damagePercentage = 0.1f;

    public override void ApplyEffect()
    {
        if (PlaneManager.instance._planeMovement.isTurning)
        {
            elapsedtime += Time.deltaTime;
            
            // after 3 seconds
            if (elapsedtime >= damageFrequency)
            {
                elapsedtime = 0;
                SpecifyDamage(damagePercentage, DamageType.Eggs);
            }
        }
    }
}