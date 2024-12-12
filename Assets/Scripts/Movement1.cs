using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement1 : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 10.0f;

    private Vector2 joystickMovement;
    private Rigidbody2D rBody;
    private bool isGrounded = true;

    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    [System.Obsolete]
    private void Update()
    {
        rBody.velocity = new Vector2(joystickMovement.x * speed, rBody.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext valueFromAction)
    {
        joystickMovement = valueFromAction.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && isGrounded)
        {
            rBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }
}
