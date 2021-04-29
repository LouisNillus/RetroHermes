using Sirenix.OdinInspector;
using UnityEngine;

public class CloudBehavior_Wind : AbstractCloudBehaviour
{
    [SerializeField] private float windForce;
    [ReadOnly] [SerializeField] Vector3 windDirection;

    // Update is called once per frame
    void Update()
    {
        if (planeManagerRef)
        {
            windDirection = transform.forward;
            float pushedDirection =
                Vector3.Dot(-planeManagerRef.transform.forward.normalized, windDirection.normalized) * windForce;
            planeManagerRef._planeMovement.direction.x += pushedDirection * Time.deltaTime;
            planeManagerRef._planeMovement.direction.z += pushedDirection * Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlaneManager>())
        {
            planeManagerRef._planeMovement.ResetSpeed();
            planeManagerRef = null;
        }
    }
}