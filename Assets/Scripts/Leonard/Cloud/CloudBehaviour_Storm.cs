using UnityEngine;

public class CloudBehaviour_Storm : AbstractCloudBehaviour
{
    private float totalDamage;
    float elapsedTime;
    private float damageFrequency = 1f;
    [SerializeField] private float planedamagePercentage = 0.05f;
    [SerializeField] private float cargodamagePercentage = 0.1f;

    private void Update()
    {
        if (planeManagerRef)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= damageFrequency)
            {
                elapsedTime = 0;
                planeManagerRef.StormDamage(planedamagePercentage, cargodamagePercentage);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlaneManager>())
        {
            elapsedTime = 0;
            if(planeManagerRef) planeManagerRef = null;
        }
    }
}