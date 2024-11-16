using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement1 : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 10.0f;

    private Vector2 joystickMovment;
    private Rigidbody2D rBody;
    void Start()
    {
       rBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rBody.linearVelocity = joystickMovment * speed;
    }
    public void OnMove(InputAction.CallbackContext valueFromAction)
    {
        joystickMovment = valueFromAction.ReadValue<Vector2>();
        
    }
    public void OnJump(InputAction.CallbackContext ctx)
    {

        //rBody.AddForce(jumpForce, ForceMode2D.Impulse);
    }
   
}
