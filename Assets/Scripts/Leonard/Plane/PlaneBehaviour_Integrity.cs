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

    public void RegenPlane() => currentIntegrity = baseIntegrity;

    public void TakeDamage()
    {
        currentIntegrity -= currentIntegrity > 0 ? 5 : 0;
        integrityText.text = currentIntegrity.ToString();
    }

    public void TakeDamage(float damageAmount)
    {
        currentIntegrity -= currentIntegrity > 0 ? damageAmount : 0;
        integrityText.text = currentIntegrity.ToString();
    }
}