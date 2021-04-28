using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo_Ore : AbstractCargo
{
    public override void ApplyEffect()
    {
        PlaneManager.instance._planeMovement.HeavyLoad();
        Debug.Log("Minerals");
    }
}