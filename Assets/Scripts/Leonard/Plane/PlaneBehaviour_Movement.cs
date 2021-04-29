using Sirenix.OdinInspector;
using UnityEngine;

public class PlaneBehaviour_Movement : AbstractPlaneBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] GameObject planeViz;

    [Space] [Header("Speed")] [SerializeField]
    private float baseSpeed;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float speedMultiplier;
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public float defaultSpeedMultiplier;

    [Space] [Header("Rotation")] [SerializeField]
    private float baseRotation;

    [SerializeField] private float rotLimit;
    private Vector3 yaw;
    private Vector3 roll;

    private bool resetAxis = false;

    [Space] [Header("Debugging")] [ReadOnly] [SerializeField]
    private float currentSpeed;

    float resetMultiplier = 200f;
    private float rotate_passedTime;
    private float rotate_reset;

    private float sway_PassedTime;
    private float swayStart, swayTarget, swayTime = 0.5f;

    [HideInInspector] public bool isFullSpeed;
    [HideInInspector] public bool isTurning;
    bool swayingPlane;

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

        if (currentSpeed > maxSpeed) currentSpeed = maxSpeed;

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
        /*rotate_passedTime += Time.deltaTime;
        float step = rotate_passedTime / rotate_reset;
        roll.z = Mathf.Lerp(, curve.Evaluate(step));*/

        roll.z = roll.z < 0 ? roll.z + resetMultiplier * Time.deltaTime : roll.z - resetMultiplier * Time.deltaTime;
        if (roll.z == 0)
        {
            rotate_passedTime = 0;
            resetAxis = false;
        }
    }

    public void ResetSpeed()
    {
        speedMultiplier = defaultSpeedMultiplier;
        direction = Vector3.forward * (currentSpeed = baseSpeed);
        isFullSpeed = false;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            direction = Vector3.forward * (currentSpeed = (baseSpeed * speedMultiplier));
            isFullSpeed = true;
        }
    }

    public void Takeoff() => speedMultiplier = defaultSpeedMultiplier;

    public void HeavyLoad() => speedMultiplier = 0.8f;

    public void KillSpeed() => speedMultiplier = 0;

    public void SwayPlane(int swayDirection)
    {
        swayTarget = (swayStart = transform.eulerAngles.y) + swayDirection;
        swayingPlane = true;
        sway_PassedTime = 0;
    }

    public void RotatePlane()
    {
        sway_PassedTime += Time.deltaTime;
        float step = sway_PassedTime / swayTime;
        yaw.y = Mathf.Lerp(swayStart, swayTarget, curve.Evaluate(step));
        roll.y = Mathf.Lerp(swayStart, swayTarget, curve.Evaluate(step));
        if (yaw.y == swayTarget)
        {
            swayingPlane = false;
            sway_PassedTime = 0;
        }
    }
}