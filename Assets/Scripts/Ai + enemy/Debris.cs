using UnityEngine;

public class Debris : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the debris hits the ground or any surface (or you can refine with specific tags or layers)
        if (collision.relativeVelocity.y > 0) // Hit from above (falling)
        {
            FlipDebris();
        }

        // Check for collision with the HandBoss (for taking damage)
        HandBoss boss = collision.gameObject.GetComponent<HandBoss>();
        if (boss != null)
        {
            boss.TakeDamage(1);
        }

        // Destroy debris after impact
        Destroy(gameObject);
    }

    private void FlipDebris()
    {
        // Flip the debris by rotating 180 degrees on the Z-axis
        transform.Rotate(0, 0, 180f);
    }
}
