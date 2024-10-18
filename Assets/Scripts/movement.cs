using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Vector2.zero;
        if (Input.GetKey(KeyCode.D))
        {
            direction = Vector2.right;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            direction = Vector2.up + Vector2.up;
        }
        float dt = Time.deltaTime;
        float speed = 3.0f;
        Vector3 change = direction * speed * dt;
        transform.position = transform.position + change;
    }
}
