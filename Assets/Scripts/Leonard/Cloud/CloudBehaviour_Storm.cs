using UnityEngine;

public class CloudBehaviour_Storm : AbstractCloudBehaviour
{
    private PlaneManager planeIntegrityRef;
    private float totalDamage;
    float elapsedTime;
    private float damageFrequency = 1f;
    [SerializeField] private float planedamagePercentage = 0.05f;
    [SerializeField] private float cargodamagePercentage = 0.1f;

    private void Update()
    {
        if (planeIntegrityRef)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= damageFrequency)
            {
                elapsedTime = 0;
                planeIntegrityRef.StormDamage(planedamagePercentage, cargodamagePercentage);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlaneManager>()) planeIntegrityRef = other.GetComponent<PlaneManager>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlaneManager>() && planeIntegrityRef)
        {
            elapsedTime = 0;
            planeIntegrityRef = null;
        }
    }
}