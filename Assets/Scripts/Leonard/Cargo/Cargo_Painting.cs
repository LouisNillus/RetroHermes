using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo_Painting : AbstractCargo
{
    public override void ApplyEffect() => Debug.Log("Painting");
}