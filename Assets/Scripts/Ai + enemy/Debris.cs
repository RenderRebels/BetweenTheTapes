using UnityEngine;
using System.Collections;

public class Debris : MonoBehaviour
{
    private bool canAffectBoss = false;
    private float effectDelay = 5f;

    private void Start()
    {
        StartCoroutine(EnableDebrisEffect());
    }

    private IEnumerator EnableDebrisEffect()
    {
        yield return new WaitForSeconds(effectDelay);
        canAffectBoss = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (canAffectBoss)
        {
            HandBoss boss = collision.gameObject.GetComponent<HandBoss>();
            if (boss != null)
            {
                boss.TakeDamage(1);
            }
        }

        if (collision.relativeVelocity.y > 0)
        {
            FlipDebris();
        }
    }

    private void FlipDebris()
    {
        transform.Rotate(0, 0, 180f);
    }
}
