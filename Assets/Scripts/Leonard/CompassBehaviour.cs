using UnityEngine;

public class CompassBehaviour : MonoBehaviour
{
    private Vector3 _rotation;

    public Vector3 rotation
    {
        get
        {
            if (PlaneManager.instance.demag_Compass) ++_rotation.z;
            else _rotation.z = PlaneManager.instance.transform.eulerAngles.y;
            return _rotation;
        }
    }

    public void Update() => transform.eulerAngles = rotation;
}