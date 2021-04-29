using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo_Painting : AbstractCargo
{
    private float startDamaging = 5f;
    private float startDamagingTime = 0f;
    
    private float damageFrequency = 1f;
    private float elapsedtime = 1f;

    private float damagePercentage = 0.05f; 
   
    public override void ApplyEffect()
    {
        Debug.Log("Updating");
        
        if (PlaneManager.instance.isInCloud)
        {
            // if in cloud for more than 5 seconds
            if (startDamagingTime >= startDamaging)
            {
                // every 1 second
                if (elapsedtime >= damageFrequency)
                {
                    elapsedtime = 0;
                    SpecifyDamage(damagePercentage, DamageType.Cloud);
                }

                elapsedtime += Time.deltaTime;
            }
            else startDamagingTime += Time.deltaTime;
        }
        
        else startDamagingTime = 0;
    }
}