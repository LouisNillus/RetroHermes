using Sirenix.OdinInspector;
using UnityEngine;

public class PlaneBehaviour_Movement : MonoBehaviour
{
    [SerializeField] GameObject planeViz;
    [SerializeField] public float baseSpeed, baseRotation, rotLimit;

    [SerializeField] float resetSpeed = 1.5f;
    private bool resetAxis = false;

    [Space] [Header("Debugging")] [ReadOnly]
    public Vector3 direction;

    private Vector3 yaw;
    private Vector3 roll;


    private void Awake()
    {
        roll = yaw = transform.eulerAngles;
        direction = Vector3.forward * baseSpeed;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow)) resetAxis = true;
        if (Input.anyKey) CheckInputs(); // update plane rotation + orientation
        if (resetAxis) ResetPlaneAxis(); // rotate plane back to original pos

        MovePlane();
    }

    private void MovePlane()
    {
        // rotation
        planeViz.transform.eulerAngles = roll;
        transform.eulerAngles = yaw;

        // movement
        transform.Translate(direction * Time.deltaTime);
    }

    private void CheckInputs()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            resetAxis = false;
            yaw.y += baseRotation * Time.deltaTime;
            roll.y += baseRotation * Time.deltaTime;
            roll.z -= roll.z > -rotLimit ? baseRotation * Time.deltaTime : 0;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            resetAxis = false;
            yaw.y -= baseRotation * Time.deltaTime;

            roll.y -= baseRotation * Time.deltaTime;
            roll.z += roll.z < rotLimit ? baseRotation * Time.deltaTime : 0;
        }
    }

    // rotate plane to indicate in which direction it is travelling
    void ResetPlaneAxis()
    {
        roll.z = roll.z < 0 ? roll.z + resetSpeed * Time.deltaTime : roll.z - resetSpeed * Time.deltaTime;

        if (roll.z == 0)
        {
            resetAxis = false;
        }
    }
}