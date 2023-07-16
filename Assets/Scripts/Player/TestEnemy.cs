using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour, IDamageable
{
    public void TakeDamage(float power)
    {
        Death();
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player")) return;

        if (other.TryGetComponent(out IKnockBackable knockBackable))
        {
            Vector3 dir = other.transform.position - transform.position;
            knockBackable.StartKnockBack(dir, 10f, 0.1f);
        }

        if (other.TryGetComponent(out IStanable stanable))
        {
            stanable.StartStan(1f);
        }
    }
}
