using Sirenix.OdinInspector;
using UnityEngine;

public class PlaneBehaviour_Movement : AbstractPlaneBehaviour
{
    [SerializeField] GameObject planeViz;
    [SerializeField] private float baseSpeed, speedMultiplier, baseRotation, rotLimit;

    [SerializeField] float resetSpeed = 1.5f;
    private bool resetAxis = false;

    [Space] [Header("Debugging")] [ReadOnly] [SerializeField]
    private float currentSpeed;

    [HideInInspector] public Vector3 direction;

    private Vector3 yaw;
    private Vector3 roll;

    private void Awake()
    {
        roll = yaw = transform.eulerAngles;
        currentSpeed = baseSpeed;

        direction = Vector3.forward * currentSpeed;
    }

    public void MovementLogic()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow)) resetAxis = true;
        if (Input.GetKeyUp(KeyCode.UpArrow)) ResetSpeed();

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

        if (Input.GetKeyDown(KeyCode.UpArrow))
            direction = Vector3.forward * (currentSpeed = (baseSpeed * speedMultiplier));
    }

    // rotate plane to indicate in which direction it is travelling
    void ResetPlaneAxis()
    {
        roll.z = roll.z < 0 ? roll.z + resetSpeed * Time.deltaTime : roll.z - resetSpeed * Time.deltaTime;
        if (roll.z == 0) resetAxis = false;
    }

    public void ResetSpeed() => direction = Vector3.forward * (currentSpeed = baseSpeed);

    public void HeavyLoad() => currentSpeed = baseSpeed / 2;

    public void KillSpeed() => currentSpeed = 0;
}