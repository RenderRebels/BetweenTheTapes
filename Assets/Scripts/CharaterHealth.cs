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

    public AudioSource audioSource; // Audio source to play sounds
    public AudioClip damageSound; // Sound effect for taking damage
    public AudioClip deathSound; // Sound effect for reaching 0 health

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

            if (currentHealth <= minHealth)
            {
                PlayDeathSound();
                Invoke("RestartScene", 1f); // Delay restart for dramatic effect
            }
            else
            {
                PlayDamageSound();
            }
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

    void PlayDamageSound()
    {
        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
    }

    void PlayDeathSound()
    {
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
    }
}
