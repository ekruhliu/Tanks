using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Moves))]

public class RotateCanon : MonoBehaviour
{
    [SerializeField] private float lookSpeed = 5f;
    public Moves moves;
    void Start() { moves = GetComponent<Moves>(); }

    void Update()
    {
        float yRotation = Input.GetAxis("Mouse X");
        Vector3 rotation = new Vector3(0f, yRotation, 0f) * lookSpeed;

        moves.Rotate(rotation);
    }
}
