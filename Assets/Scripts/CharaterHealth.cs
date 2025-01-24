using UnityEngine;

public class CharaterHealth : MonoBehaviour
{
    const int maxHealth = 5;
    const int minHealth = 0;
    const int damage = 1;
    int currentHealth = maxHealth;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            currentHealth -= damage;
            Debug.Log("health" + currentHealth);
        }

        if (currentHealth <= minHealth)
        {
            Destroy(gameObject);
        }
    }
}
