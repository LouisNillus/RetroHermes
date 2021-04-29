using Sirenix.OdinInspector;
using UnityEngine;

public class PlaneBehaviour_Movement : AbstractPlaneBehaviour
{
    [SerializeField] GameObject planeViz;
    [SerializeField] private float baseSpeed, speedMultiplier, baseRotation, rotLimit;

    public float defaultSpeedMultiplier;

    [SerializeField] float resetSpeed = 1.5f;
    private bool resetAxis = false;

    [Space] [Header("Debugging")] [ReadOnly] [SerializeField]
    private float currentSpeed;

    [HideInInspector] public Vector3 direction;

    private Vector3 yaw;
    private Vector3 roll;

    private float t, step;
    bool swayingPlane;
    private float swayStart, swayTarget, swayTime = .5f;

    public bool isFullSpeed;
    public bool isTurning;

    private void Awake()
    {
        defaultSpeedMultiplier = speedMultiplier;
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

        if (swayingPlane) RotatePlane();

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
            isTurning = true;
            resetAxis = false;
            yaw.y += baseRotation * Time.deltaTime;
            roll.y += baseRotation * Time.deltaTime;
            roll.z -= roll.z > -rotLimit ? baseRotation * Time.deltaTime : 0;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            isTurning = true;
            resetAxis = false;
            yaw.y -= baseRotation * Time.deltaTime;

            roll.y -= baseRotation * Time.deltaTime;
            roll.z += roll.z < rotLimit ? baseRotation * Time.deltaTime : 0;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Vector3.forward * (currentSpeed = (baseSpeed * speedMultiplier));
            isFullSpeed = true;
        }
    }

    // rotate plane to indicate in which direction it is travelling
    void ResetPlaneAxis()
    {
        isTurning = false;
        roll.z = roll.z < 0 ? roll.z + resetSpeed * Time.deltaTime : roll.z - resetSpeed * Time.deltaTime;
        if (roll.z == 0) resetAxis = false;
    }

    public void ResetSpeed()
    {
        direction = Vector3.forward * (currentSpeed = baseSpeed);
        isFullSpeed = false;
    }

    public void Takeoff() => speedMultiplier = defaultSpeedMultiplier;

    public void HeavyLoad() => speedMultiplier = 0.8f;

    public void KillSpeed() => speedMultiplier = 0;

    public void SwayPlane(int swayDirection)
    {
        swayTarget = (swayStart = transform.eulerAngles.y) + swayDirection;
        swayingPlane = true;
        t = 0;
    }

    public void RotatePlane()
    {
        t += Time.deltaTime;
        step = t / swayTime;
        yaw.y = Mathf.Lerp(swayStart, swayTarget, step);
        roll.y = Mathf.Lerp(swayStart, swayTarget, step);
        if (yaw.y == swayTarget) swayingPlane = false;
    }
}