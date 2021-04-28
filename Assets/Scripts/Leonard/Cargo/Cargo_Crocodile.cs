using UnityEngine;

public class Cargo_Crocodile : AbstractCargo
{
    private float elapsedTime;
    [SerializeField] private float thrashFrequency = 10f;
    
    public override void ApplyEffect()
    {
        Debug.Log("Crocodile");
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= thrashFrequency)
        {
            // TODO : sway the plane in a random direction
        }
    }
}
