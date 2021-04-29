using System;
using UnityEngine;

public class CloudBehavior_Basic : AbstractCloudBehaviour
{
    private void Update()
    {
        if (planeManagerRef) 
            planeManagerRef.isInCloud = true;
    }
}