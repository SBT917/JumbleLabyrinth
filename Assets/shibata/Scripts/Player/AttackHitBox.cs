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
        if(other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(attackable.Power, transform);
        }

        //if (other.TryGetComponent(out IDamageable target))
        //{
        //    target.TakeDamage(attackable.Power);
        //}

        //if(other.TryGetComponent(out IKnockBackable knockBackable))
        //{
        //    Vector3 dir = other.transform.position - transform.position;
        //    knockBackable.StartKnockBack(dir, attackable.KnockForce, 0.1f);
        //}
    }
}
