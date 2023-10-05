using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    IAttackable attackable;

    void Awake()
    {
        transform.parent.TryGetComponent(out attackable);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.TryGetComponent(out Enemy enemy);
            enemy.TakeDamage(attackable.Power, transform.parent, 2, 0.5f);
        }
        else if(other.gameObject.CompareTag("Projectile"))
        {
            other.TryGetComponent(out EnemyProjectile projectile);
            projectile.ShotDown();
        }


    }
}
