using UnityEngine;
using System.Collections;
public class HandBoss : MonoBehaviour
{
    public Transform target;
    public float swoopSpeed = 10f;
    public float slamSpeed = 15f;
    public float returnSpeed = 5f;
    public float attackDelay = 2f;

    private Vector3 startPosition;
    private bool isAttacking = false;
    private Vector3 swoopTarget;

    void Start()
    {
        // Save the starting position
        startPosition = transform.position;
        // Start the attack loop
        StartCoroutine(AttackLoop());
    }

    IEnumerator AttackLoop()
    {
        while (true)
        {
            // Wait before the first attack
            yield return new WaitForSeconds(attackDelay);

            // Randomly choose between swoop and slam attack
            if (Random.value > 0.5f)
            {
                SetSwoopTarget();
                yield return StartCoroutine(SwoopDown());
            }
            else
            {
                yield return StartCoroutine(SlamDown());
            }

            // Return to start position
            yield return StartCoroutine(ReturnToStart());
        }
    }

    void SetSwoopTarget()
    {
        // Set the target position when the swoop attack triggers
        swoopTarget = target.position;
    }

    IEnumerator SwoopDown()
    {
        isAttacking = true;
        while (Vector3.Distance(transform.position, swoopTarget) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, swoopTarget, swoopSpeed * Time.deltaTime);
            yield return null;
        }
        isAttacking = false;
    }

    IEnumerator SlamDown()
    {
        isAttacking = true;
        Vector3 slamPosition = new Vector3(target.position.x, target.position.y - 2f, target.position.z);
        while (Vector3.Distance(transform.position, slamPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, slamPosition, slamSpeed * Time.deltaTime);
            yield return null;
        }
        isAttacking = false;
    }

    IEnumerator ReturnToStart()
    {
        while (Vector3.Distance(transform.position, startPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, returnSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
