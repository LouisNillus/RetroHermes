using UnityEngine;

public class CloudBehaviour_Storm : AbstractCloudBehaviour
{
    private PlaneBehaviour_Integrity planeIntegrityRef;
    private float totalDamage;
    float elapsedTime;
    private float damageFrequency = 1f;
    [SerializeField] private float damagePercentage = 0.05f;

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= damageFrequency)
        {
            elapsedTime = 0;
            planeIntegrityRef?.TakeDamage(planeIntegrityRef.baseIntegrity * damagePercentage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlaneBehaviour_Integrity>()) planeIntegrityRef = other.GetComponent<PlaneBehaviour_Integrity>();
    } 
    
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlaneBehaviour_Integrity>() && planeIntegrityRef) planeIntegrityRef = null;
    }
}
