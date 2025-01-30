using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Required for UI elements

public class CharacterHealth : MonoBehaviour
{
    const int maxHealth = 3;
    const int minHealth = 0;
    const int damage = 1;
    int currentHealth = maxHealth;

    public Image healthHUD; // UI Image that displays the health
    public Sprite[] healthSprites; // Array of sprites for different health levels

    void Start()
    {
        UpdateHealthHUD();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
            Debug.Log("Health: " + currentHealth);
            UpdateHealthHUD();
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

    void UpdateHealthHUD()
    {
        if (healthHUD != null && healthSprites.Length > 0)
        {
            int spriteIndex = Mathf.Clamp(currentHealth - 1, 0, healthSprites.Length - 1);
            healthHUD.sprite = healthSprites[spriteIndex];
        }
    }
}
