using Sirenix.OdinInspector;
using UnityEngine;

public class WindBehaviour : MonoBehaviour
{
    [SerializeField] private float windForce;
    
    [Space][Header("Debugging")]
    [ReadOnly] [SerializeField] PlaneBehaviour_Movement planeBehaviourRef;
    [ReadOnly] [SerializeField] Vector3 windDirection;

    // Update is called once per frame
    void Update()
    {
        windDirection = transform.forward;

        if (planeBehaviourRef)
        {
            float pushedDirection = Vector3.Dot(-planeBehaviourRef.transform.forward.normalized, windDirection.normalized) * windForce;
            planeBehaviourRef.direction.x += pushedDirection * Time.deltaTime;
            planeBehaviourRef.direction.z += pushedDirection * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlaneBehaviour_Movement>())
            planeBehaviourRef = other.GetComponent<PlaneBehaviour_Movement>();
    }

    private void OnTriggerExit(Collider other)
    {
        planeBehaviourRef.direction = Vector3.forward * planeBehaviourRef.baseSpeed;
        planeBehaviourRef = null;
    }
}