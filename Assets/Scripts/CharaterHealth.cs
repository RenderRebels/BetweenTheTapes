using UnityEngine;

public class CharaterHealth : MonoBehaviour
{
    GameObject Player;

    float[] numbers = { 0f, 1f, 2f, 3f, 4f, 5f };

    void Start()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        int maxHealth = 5;
        int minHealth = 0;
        int damage = -1;
        int currentHealth = maxHealth + damage;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("health" + currentHealth);
        }
        if (currentHealth <= minHealth);
        {
            Destroy(Player);
        }
    }

    void Update()
    {
        //if player ridgid body collides with enemy ridgid body, player takes dam and losses a heart

        //if player health is down to zero player dies
    }
}
