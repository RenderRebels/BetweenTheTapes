using UnityEngine;

public class slimemovment1 : MonoBehaviour
{
    public GameObject player;
    BoxCollider2D collision2D;
    Vector2 direction = Vector2.right;

    float speed = 0.5f;
    float patrolrange = 5.0f;
    float startx;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = Vector2.zero;
        float startx = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float leftlimit = startx - patrolrange;
        float rightlimit = startx + patrolrange;

        float dt = Time.deltaTime;
        Vector3 change = direction * dt * speed;
        transform.position += change;

        if (transform.position.x >= rightlimit)
        {
            direction.x = - direction.x;
        }
        if (transform.position.x <= leftlimit)
        {
            direction.x = - direction.x;
        }

    }
}