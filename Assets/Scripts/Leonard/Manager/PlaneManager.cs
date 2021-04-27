using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    [SerializeField] private AbstractPlaneBehaviour[] _planeBehaviours;

    public void StormDamage()
    {
        // TODO : damage to hull integrity
        // TODO : damage to all goods
    }
}
