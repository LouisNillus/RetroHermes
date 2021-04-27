using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo_Explosives : AbstractCargo
{
    public override void ApplyEffect() => Debug.Log("Explosives");
}