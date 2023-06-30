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
}
