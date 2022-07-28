using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Rigidbody rb;
    [SerializeField] private DynamicJoystick joystick;
    [SerializeField] private float speed;
    private Vector3 movementDirection;
    private void FixedUpdate()
    {
        rb.velocity = movementDirection * speed;
    }
    private void Update()
    {
        movementDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized;

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(rb.velocity.x, 0, rb.velocity.z));
        }
        if (movementDirection == Vector3.zero)
        {
            rb.velocity = Vector3.zero;
        }
    }
}
