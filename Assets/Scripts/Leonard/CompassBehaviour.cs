using System;
using UnityEngine;

public class CompassBehaviour : MonoBehaviour
{
    [SerializeField] private PlaneManager planeManager;
    private Vector3 rotation;

    private void Update()
    {
        rotation.z = planeManager.transform.eulerAngles.y;
        transform.eulerAngles = rotation;
    }
}