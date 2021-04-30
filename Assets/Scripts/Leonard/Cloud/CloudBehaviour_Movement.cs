using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehaviour_Movement : AbstractCloudBehaviour
{
    [SerializeField] private float travelDistance, travelTime;
    private float elapsedTime, step;
    private Vector3 startPos, targetPos;
    private bool right;

    private void Awake()
    {
        startPos = transform.position;
        //targetPos = new Vector3(transform.position.x + (travelDistance * 0.5f), transform.position.y, transform.position.z);
        targetPos = transform.position + transform.right * (travelDistance * 0.5f);
        right = true;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        step = elapsedTime / travelTime;
        transform.position = Vector3.Lerp(startPos, targetPos, step);

        if(elapsedTime >= travelTime) Switch();
    }

    void Switch()
    {
        startPos = transform.position;
        if (right)
        {
            //targetPos = new Vector3(transform.position.x - travelDistance, transform.position.y, transform.position.z);
            targetPos = transform.position + transform.right * (-travelDistance);
            right = false;
        }

        else
        {
            //targetPos = new Vector3(transform.position.x + travelDistance, transform.position.y, transform.position.z);
            targetPos = transform.position + transform.right * (travelDistance);
            right = true;
        }

        elapsedTime = 0;
    }

    
    private Vector3 posRight;
    private Vector3 posLeft;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        if (!Application.isPlaying)
        {
            //posRight = new Vector3(transform.position.x + (travelDistance * 0.5f), transform.position.y, transform.position.z);
            //posLeft = new Vector3(transform.position.x - (travelDistance * 0.5f), transform.position.y, transform.position.z);
            posRight = transform.position + transform.right * (travelDistance * 0.5f);
            posLeft = transform.position + transform.right * (-travelDistance * 0.5f);
        }
        
        Gizmos.DrawSphere(posLeft, 10);
        Gizmos.DrawSphere(posRight, 10);
        Gizmos.DrawLine(posLeft, posRight);
    }
} 