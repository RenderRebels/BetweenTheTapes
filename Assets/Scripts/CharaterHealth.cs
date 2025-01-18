using UnityEngine;

public class CharaterHealth : MonoBehaviour
{
    GameObject Player;

    float[] numbers = { 0f, 1f, 2f, 3f, 4f, 5f };
    float maxHealth = 5f;
    float minHealth = 0f;
    float currenthealth = 5f;

    void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if ()
            {
                
            }
        }
    }

    void Update()
    {
        //if player ridgid body collides with enemy ridgid body, player takes dam and losses a heart

        //if player health is down to zero player dies
    }
}
