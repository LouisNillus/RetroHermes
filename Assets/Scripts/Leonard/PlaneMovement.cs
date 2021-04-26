using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float rotSpeed;
    private Vector3 rotation;

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow)) rotation.y += rotSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftArrow)) rotation.y -= rotSpeed * Time.deltaTime;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.eulerAngles = rotation;
    }
}