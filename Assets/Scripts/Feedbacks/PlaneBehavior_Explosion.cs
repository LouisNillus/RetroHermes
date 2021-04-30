using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneBehavior_Explosion : MonoBehaviour
{
    public ParticleSystem ps;

    public void Explode()
    {
        Instantiate(ps, transform.position, Quaternion.identity);
    }
}
