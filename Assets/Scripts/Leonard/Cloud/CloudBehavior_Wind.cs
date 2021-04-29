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
            float pushedDirection = Vector3.Dot(-planeManagerRef.transform.forward.normalized, windDirection.normalized) * windForce;
            planeManagerRef._planeMovement.offsetDirection += pushedDirection * Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlaneManager>())
        {
            planeManagerRef._planeMovement.EaseOut_Cloud();
            planeManagerRef = null;
        }
    }

    BoxCollider boxCollider;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if (boxCollider == null)
        {
            boxCollider = GetComponent<BoxCollider>();
        }
        
        //Draw Arrow
        Vector3 pos = transform.position + (transform.forward * -100) + new Vector3(0,200,0);
        
        Gizmos.DrawLine(transform.position + new Vector3(0,200,0), pos );
        Gizmos.DrawSphere(pos, 10);
        
        //Draw cube
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(boxCollider.size.x, boxCollider.size.y, boxCollider.size.z));
    }
}