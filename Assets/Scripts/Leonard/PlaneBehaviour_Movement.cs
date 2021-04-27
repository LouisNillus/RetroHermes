using Sirenix.OdinInspector;
using UnityEngine;

public class PlaneBehaviour_Movement : MonoBehaviour
{
    [SerializeField] public float baseSpeed, baseRotation;
    [SerializeField] GameObject planeViz;

    [Space][Header("Debugging")]
    [ReadOnly] public Vector3 direction;
    private Vector3 yaw;
    private Vector3 roll;
    private Vector3 prevRoll;

    private float t = 0.0f, interpTime = 0.5f;
    private bool resetAxis = false;

    private void Awake()
    {
        roll = yaw = transform.eulerAngles;
        direction = Vector3.forward * baseSpeed;
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 20);
        
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            resetAxis = true;
            prevRoll = planeViz.transform.eulerAngles;
        }

        CheckInputs(); // update plane rotation + orientation
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
            roll.z -= baseRotation * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            resetAxis = false;
            yaw.y -= baseRotation * Time.deltaTime;

            roll.y -= baseRotation * Time.deltaTime;
            roll.z += baseRotation * Time.deltaTime;
        }
    }

    // rotate plane to indicate in which direction it is travelling
    void ResetPlaneAxis()
    {
        roll.z = Mathf.Lerp(prevRoll.z, 0, t);

        t += interpTime * Time.deltaTime;

        if (t >= 1.0f || roll.z == 0)
        {
            resetAxis = false;
            t = 0.0f;
        }
    }
}