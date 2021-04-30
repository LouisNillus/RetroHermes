using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class PlaneBehaviour_Integrity : AbstractPlaneBehaviour
{
    [SerializeField] public float baseIntegrity;
    [SerializeField] Text integrityText;

    [Space] [Header("Debugging")] [ReadOnly] [SerializeField]
    public float currentIntegrity;

    private void Awake()
    {
        currentIntegrity = baseIntegrity;
        integrityText.text = currentIntegrity.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) TakeDamage();
    }

    public void RegenPlane()
    {
        currentIntegrity = baseIntegrity;
        integrityText.text = currentIntegrity.ToString();
    }

    public void TakeDamage()
    {
        currentIntegrity -= 5;
        integrityText.text = ((int)currentIntegrity).ToString();

        if (currentIntegrity <= 0)
        {
            PlaneManager.instance._planeExplosion.Explode();
            PlaneManager.instance.mustRespawn = true;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentIntegrity -= damageAmount;
        integrityText.text = ((int)currentIntegrity).ToString();
        

        if (currentIntegrity <= 0)
        {
            PlaneManager.instance._planeExplosion.Explode();
            PlaneManager.instance.mustRespawn = true;
        }
    }
}