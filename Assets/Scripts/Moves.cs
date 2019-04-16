using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody))]

public class Moves : MonoBehaviour
{
    private Camera cam;
    public GameObject canon;
    private Rigidbody rb;
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 rotationCam = Vector3.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    private void FixedUpdate()
    {
        PerformMove();
        PerformRotation();
    }

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }
    
    void PerformMove()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
            canon.GetComponent<Rigidbody>().MovePosition(canon.GetComponent<Rigidbody>().position + velocity * Time.fixedDeltaTime);
        }
    }

    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if(cam != null)
            cam.transform.Rotate(-rotationCam);
    }
}
