using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Wind_Behaviour : MonoBehaviour
{
    [SerializeField] private float windForce;
    [SerializeField] PlaneMovement planeRef;

    [SerializeField] private int updateFrequency;
    private int tick;

    [SerializeField] Vector3 windDirection;

    // Update is called once per frame
    void Update()
    {
        windDirection = transform.forward;

        if (planeRef)
        {
            float pushedDirection = Vector3.Dot(planeRef.transform.forward.normalized, windDirection.normalized) * windForce;
            Debug.Log(pushedDirection);
            
            planeRef.yaw.y += pushedDirection * Time.deltaTime;
            planeRef.roll.y += pushedDirection * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlaneMovement>()) planeRef = other.GetComponent<PlaneMovement>();
    }

    private void OnTriggerExit(Collider other)
    {
        planeRef = null;
    }
}