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
        targetPos = new Vector3(transform.position.x + (travelDistance * 0.5f), transform.position.y, transform.position.z);
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
            targetPos = new Vector3(transform.position.x - travelDistance, transform.position.y, transform.position.z);
            right = false;
        }

        else
        {
            targetPos = new Vector3(transform.position.x + travelDistance, transform.position.y, transform.position.z);
            right = true;
        }

        elapsedTime = 0;
    }
}