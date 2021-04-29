using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo_Painting : AbstractCargo
{
    private float startDamaging = 5f;
    private float elapsedtime = 0f;
    
    public override void ApplyEffect()
    {
        if (PlaneManager.instance.isInCloud)
        {
            elapsedtime += Time.deltaTime;

            if (elapsedtime >= startDamaging)
            {
                elapsedtime = 0;
                
            }
        }
    }
}