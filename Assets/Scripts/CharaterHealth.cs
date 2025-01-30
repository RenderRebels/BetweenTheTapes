using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterHealth : MonoBehaviour
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
            Debug.Log("Health: " + currentHealth);
        }

        if (currentHealth <= minHealth)
        {
            RestartScene();
        }
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}