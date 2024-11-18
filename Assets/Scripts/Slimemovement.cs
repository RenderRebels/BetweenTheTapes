using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slimemovment : MonoBehaviour
{
    public float walkSpeed = 1.0f;      // Walkspeed
    public float wallLeft = 0.0f;       // Define wallLeft
    public float wallRight = 5.0f;      // Define wallRight
    float walkingDirection = 1.0f;
    Vector2 walkAmount;
    float originalX; // Original float value

    /*
    public float speed = 1.0f;
    public float distance = 5.0f;
    public bool movingleft = true;
    public Transform groundDectection;
    */
    void Start()
    {
        this.originalX = this.transform.position.x;
    }

    void Update()
    {
        walkAmount.x = walkingDirection * walkSpeed * Time.deltaTime;
        if (walkingDirection > 0.0f && transform.position.x >= wallRight)
        {
            walkingDirection = -1.0f;
        }
        else if (walkingDirection < 0.0f && transform.position.x <= wallLeft)
        {
            walkingDirection = 1.0f;
        }
        transform.Translate(walkAmount);

    }
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            if ()
        }

    }
    */

}
