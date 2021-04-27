using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class PlaneBehaviour_Integrity : MonoBehaviour
{
    [SerializeField] float baseIntegrity;
    [SerializeField] Text integrityText;
    
    [Space][Header("Debugging")]
    [ReadOnly] [SerializeField] float currentIntegrity;

    private void Awake()
    {
        currentIntegrity = baseIntegrity;
        integrityText.text = currentIntegrity.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) TakeDamage();
    }

    public void TakeDamage()
    {
        currentIntegrity -= currentIntegrity > 0 ? 5 : 0;
        integrityText.text = currentIntegrity.ToString();
    }

    public void TakeDamage(int damageAmount)
    {
        currentIntegrity -= currentIntegrity > 0 ? damageAmount : 0;
        integrityText.text = currentIntegrity.ToString();
    }
}