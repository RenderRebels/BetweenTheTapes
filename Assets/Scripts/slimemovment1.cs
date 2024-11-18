using UnityEngine;

public class Slimemovment1 : MonoBehaviour
{
    private float moveSpeed = 3f;
    private Vector2 movementDirection = Vector2.right;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        Vector2 movement = movementDirection.normalized * moveSpeed;
        rb.linearVelocity = movement;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.name == "ground")
        {

            movementDirection = (movementDirection == Vector2.right) ? Vector2.left : Vector2.right;
        }


        if (collision.gameObject.name == "AngrySlime")
        {

            movementDirection = (movementDirection == Vector2.right) ? Vector2.left : Vector2.right;
        }
    }
}
