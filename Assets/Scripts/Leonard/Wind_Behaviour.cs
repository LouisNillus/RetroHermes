using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Wind_Behaviour : MonoBehaviour
{
    [SerializeField] private float windForce;
    [SerializeField] PlaneMovement planeRef;

    [SerializeField] Vector3 windDirection;

    // Update is called once per frame
    void Update()
    {
        windDirection = transform.forward;

        if (planeRef)
        {
            float pushedDirection = Vector3.Dot(-planeRef.transform.forward.normalized, windDirection.normalized) * windForce;
            planeRef.direction.x += pushedDirection * Time.deltaTime;
            planeRef.direction.z += pushedDirection * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlaneMovement>()) planeRef = other.GetComponent<PlaneMovement>();
    }

    private void OnTriggerExit(Collider other)
    {
        planeRef.direction = Vector3.forward * planeRef.speed;
        planeRef = null;
    }
}