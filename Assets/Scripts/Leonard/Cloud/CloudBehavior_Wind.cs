using Sirenix.OdinInspector;
using UnityEngine;

public class CloudBehavior_Wind : AbstractCloudBehaviour
{
    [SerializeField] private float windForce;
    
    [Space][Header("Debugging")]
    [ReadOnly] [SerializeField] PlaneBehaviour_Movement planeBehaviourRef;
    [ReadOnly] [SerializeField] Vector3 windDirection;

    // Update is called once per frame
    void Update()
    {
        if (planeBehaviourRef)
        {
            float pushedDirection = Vector3.Dot(-planeBehaviourRef.transform.forward.normalized, windDirection.normalized) * windForce;
            planeBehaviourRef.direction.x += pushedDirection * Time.deltaTime;
            planeBehaviourRef.direction.z += pushedDirection * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        windDirection = transform.forward;
        
        if (other.GetComponent<PlaneBehaviour_Movement>())
            planeBehaviourRef = other.GetComponent<PlaneBehaviour_Movement>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (planeBehaviourRef)
        {
            planeBehaviourRef.ResetSpeed();
            planeBehaviourRef = null;
        }
    }
}