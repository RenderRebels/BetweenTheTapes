using UnityEngine;

public class CharaterHealth : MonoBehaviour
{
    GameObject Player;

    //float[] numbers = { 0f, 1f, 2f, 3f, 4f, 5f };

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
        if (currentHealth <= minHealth)
        {
            Destroy(Player);
        }
    }
}
