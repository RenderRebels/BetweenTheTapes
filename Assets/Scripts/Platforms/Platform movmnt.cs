using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2.0f;
    public bool moveOnContact = true;
    public bool stopAtBPosition = false;

    private Vector3 target;
    private bool movingToB = true;

    void Start()
    {
        target = pointB.position;
    }

    void Update()
    {
        if (!moveOnContact)
        {
            MovePlatform();
        }
    }

    void MovePlatform()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            if (movingToB && !stopAtBPosition)
            {
                target = pointA.position;
                movingToB = false;
            }
            else if (!movingToB && !stopAtBPosition)
            {
                target = pointB.position;
                movingToB = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (moveOnContact && collision.gameObject.CompareTag("Player"))
        {
            moveOnContact = false;
            MovePlatform();
        }
    }
}