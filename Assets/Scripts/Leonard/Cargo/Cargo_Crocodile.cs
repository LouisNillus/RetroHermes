using UnityEngine;

public class Cargo_Crocodile : AbstractCargo
{
    private float elapsedTime;
    [SerializeField] private float thrashFrequency = 10f;
    [SerializeField] private int minAngle =45, maxAngle = 90;
    
    public override void ApplyEffect()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= thrashFrequency)
        {
            PlaneManager.instance._planeMovement.SwayPlane(Random.Range(minAngle, maxAngle));
            elapsedTime = 0;
            // TODO : sway the plane in a random direction
        }
    }
}
