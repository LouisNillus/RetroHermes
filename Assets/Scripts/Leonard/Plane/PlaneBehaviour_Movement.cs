using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlaneBehaviour_Movement : AbstractPlaneBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] GameObject planeViz;

    [Space] [Header("Speed")] [SerializeField]
    private float baseSpeed;

    [SerializeField] private float speedMultiplier;

    private Vector3 _direction = Vector3.zero;

    [HideInInspector]
    public Vector3 direction
    {
        get
        {
            if (stopMoving) _direction = Vector3.zero;
            else if (heavyLoad) _direction = Vector3.forward * (currentSpeed = (baseSpeed * 0.8f));
            else if (Input.GetKey(KeyCode.UpArrow)) _direction = Vector3.forward * (currentSpeed = (baseSpeed * speedMultiplier));
            return _direction;
        }
        set
        {
            _direction.x = value.x;
            _direction.y = value.y;
            _direction.z = value.z;
        }
    }

    private bool heavyLoad, stopMoving;

    [HideInInspector] public float defaultSpeedMultiplier;

    [Space] [Header("Rotation")] [SerializeField]
    private float baseRotation;

    [SerializeField] private float rotLimit;
    private Vector3 yaw;
    private Vector3 roll;
    private Vector3 planePos;

    private bool resetAxis = false;

    [Space] [Header("Debugging")] [ReadOnly] [SerializeField]
    private float currentSpeed;

    float resetMultiplier = 200f;
    private float rotate_passedTime;
    private float rotate_reset;

    private float sway_PassedTime;
    private float swayStart, swayTarget, swayTime = 0.5f;

    private float forward_passedTime;
    private float startPos, targetPos, defaultPos;

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

    private void Start()
    {
        planePos = planeViz.transform.position;
        defaultPos = planePos.z;
    }

    public void MovementLogic()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow)) resetAxis = true;
        if (Input.GetKeyUp(KeyCode.UpArrow)) ResetMovement();

        if (Input.anyKey) CheckInputs(); // update plane rotation + orientation
        if (resetAxis) ResetPlaneAxis(); // rotate plane back to original pos

        if (swayingPlane) RotatePlane();
        if (isFullSpeed) MovePlaneToCenter();

        MovePlane();
    }

    private void MovePlane()
    {
        // rotation
        planeViz.transform.eulerAngles = roll;
        transform.eulerAngles = yaw;

        if (currentSpeed > baseSpeed * speedMultiplier) currentSpeed = baseSpeed * speedMultiplier;

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
            startPos = defaultPos;
            targetPos = defaultPos + 50;
            isFullSpeed = true;
        }
    }

    // rotate plane to indicate in which direction it is travelling
    void ResetPlaneAxis()
    {
        isTurning = false;
        roll.z = roll.z < 0 ? roll.z + resetMultiplier * Time.deltaTime : roll.z - resetMultiplier * Time.deltaTime;
        if (roll.z == 0)
        {
            rotate_passedTime = 0;
            resetAxis = false;
        }
    }

    public void ResetMovement()
    {
        isFullSpeed = false;
        heavyLoad = false;
        stopMoving = false;
    }

    public void HeavyLoad() => heavyLoad = true;

    public void KillSpeed() => stopMoving = true;

    public void SwayPlane(int swayDirection)
    {
        swayTarget = (swayStart = transform.eulerAngles.y) + swayDirection;
        swayingPlane = true;
        sway_PassedTime = 0;
    }

    private void MovePlaneToCenter()
    {
        forward_passedTime += Time.deltaTime;
        planeViz.transform.position =
            new Vector3(planePos.x, planePos.y, Mathf.Lerp(startPos, targetPos, forward_passedTime));
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