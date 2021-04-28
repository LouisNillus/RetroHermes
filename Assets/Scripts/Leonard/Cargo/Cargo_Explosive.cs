using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo_Explosive : AbstractCargo
{
    public override void ApplyEffect() => Debug.Log("Explosives");
}