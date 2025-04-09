using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    const int maxHealth = 3;
    const int minHealth = 0;
    const int damage = 1;
    int currentHealth = maxHealth;

    public Image healthHUD;
    public Sprite[] healthSprites;
    public AudioSource audioSource;
    public AudioClip damageSound;
    public AudioClip deathSound;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        UpdateHealthHUD();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(damage);
        }

        if (collision.gameObject.CompareTag("Spike"))
        {
            TakeDamage(damage);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
        Debug.Log("Health: " + currentHealth);
        UpdateHealthHUD();

        if (currentHealth <= minHealth)
        {
            PlayDeath();
        }
        else
        {
            PlayDamageSound();
        }
    }

    void PlayDeath()
    {
        if (animator != null)
        {
            animator.SetTrigger("Death"); // Trigger the Death animation
        }
        PlayDeathSound();
        RestartScene(); // Instantly restart the scene
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