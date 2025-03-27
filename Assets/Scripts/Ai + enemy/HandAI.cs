using Unity.VisualScripting;
using UnityEngine;

public class HandAI : MonoBehaviour
{
   
    public Rigidbody2D rb;
    private Vector2 movement;
    private bool isPlayerInRange = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isPlayerInRange)
        {
            TrackPlayer();
        }
    }

    void TrackPlayer()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }
   
}
