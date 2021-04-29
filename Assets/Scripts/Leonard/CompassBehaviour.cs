using UnityEngine;

public class CompassBehaviour : MonoBehaviour
{
    public Vector3 rotation;

    public void Update() => transform.eulerAngles = rotation;

    public void TrackPlayerNorth() => rotation.z = PlaneManager.instance.transform.eulerAngles.y;
    
    public void RandomCompass() => ++rotation.z;
}