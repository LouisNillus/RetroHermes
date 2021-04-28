using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo_Bottles : AbstractCargo
{
    public override void ApplyEffect() => Debug.Log("Glass");
}