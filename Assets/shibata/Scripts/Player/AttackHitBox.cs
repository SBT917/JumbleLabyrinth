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
            enemy.TakeDamage(attackable.Power, transform, 5, 0.5f);
        }
    }
}
