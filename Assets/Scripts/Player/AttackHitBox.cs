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
        IDamageable target;
        if (other.TryGetComponent(out target))
        {
            target.TakeDamage(attackable.GetPower());
        }
    }
}
