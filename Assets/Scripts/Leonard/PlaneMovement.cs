using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float rotSpeed;
    private Vector3 orientation, rotation, lastRot;
    private bool resetAxis = false;
    private float t = 0.0f, interpTime = 0.5f;

    [SerializeField] GameObject planeViz;
    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            resetAxis = true;
            lastRot =  planeViz.transform.eulerAngles;
        }
        
        CheckInputs();
        if(resetAxis) ResetPlaneAxis();
        
        // rotation
        planeViz.transform.eulerAngles = rotation;
        transform.eulerAngles = orientation;
        
        // movement
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }

    private void CheckInputs()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            resetAxis = false;
            orientation.y += rotSpeed * Time.deltaTime;
            rotation.y += rotSpeed * Time.deltaTime;
            rotation.z -= rotSpeed * Time.deltaTime;
        }
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            resetAxis = false;
            orientation.y -= rotSpeed * Time.deltaTime;
            rotation.y -= rotSpeed * Time.deltaTime;
            rotation.z += rotSpeed * Time.deltaTime;
        }
    }

    void ResetPlaneAxis()
    {
        rotation.z = Mathf.Lerp(lastRot.z, 0, t);

        t += interpTime * Time.deltaTime;

        if (t >= 1.0f || rotation.z == 0)
        {
            resetAxis = false;
            t = 0.0f;
        }
    }
}