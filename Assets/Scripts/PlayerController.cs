using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Rigidbody rb;
    [SerializeField] private DynamicJoystick joystick;
    [SerializeField] private float speed;
    private Vector3 movementDirection;
    private void FixedUpdate()
    {
        rb.linearVelocity = movementDirection * speed;
    }
    private void Update()
    {
        movementDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized;

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z));
        }
        if (movementDirection == Vector3.zero)
        {
            rb.linearVelocity = Vector3.zero;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
