using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    
    //public float speed = 5.0f;
    public float jumpForce = 10.0f;

    private Rigidbody2D rBody;
    public 

    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Vector2 direction = Vector2.zero;
        if (Input.GetKey(KeyCode.D))
        {
            direction = Vector2.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction = Vector2.left;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            direction = Vector2.up + Vector2.up;
        }
        float dt = Time.deltaTime;
        float speed = 5.0f;
        Vector3 change = direction * speed * dt;
        transform.position = transform.position + change;
       //rBody.AddForce(jumpForce);
    }
  
    

}
