using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Serialization;

public class PlaneMovement : MonoBehaviour
{
    [SerializeField] public float speed, rotSpeed;
    [SerializeField] GameObject planeViz;

    [HideInInspector] public Vector3 yaw;
    [HideInInspector] public Vector3 roll;
    [HideInInspector] public Vector3 prevRoll;
    
    private float t = 0.0f, interpTime = 0.5f;
    private bool resetAxis = false;

    private void Awake()
    {
        roll = yaw = transform.eulerAngles;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            resetAxis = true;
            prevRoll = planeViz.transform.eulerAngles;
        }

        CheckInputs(); // update plane rotation + orientation
        if (resetAxis) ResetPlaneAxis(); // rotate plane back to original pos

        MovePlane();
    }

    private void MovePlane()
    {
        // rotation
        planeViz.transform.eulerAngles = roll;
        transform.eulerAngles = yaw;
        
        // movement
        transform.Translate(transform.forward * (speed * Time.deltaTime));
    }

    private void CheckInputs()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            resetAxis = false;
            yaw.y += rotSpeed * Time.deltaTime;
            
            roll.y += rotSpeed * Time.deltaTime;
            roll.z -= rotSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            resetAxis = false;
            yaw.y -= rotSpeed * Time.deltaTime;
            
            roll.y -= rotSpeed * Time.deltaTime;
            roll.z += rotSpeed * Time.deltaTime;
        }
    }

    // rotate plane to indicate in which direction it is travelling
    void ResetPlaneAxis()
    {
        roll.z = Mathf.Lerp(prevRoll.z, 0, t);

        t += interpTime * Time.deltaTime;

        if (t >= 1.0f || roll.z == 0)
        {
            resetAxis = false;
            t = 0.0f;
        }
    }
}