using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    bool Enable { get; set; }
    public void Attack();
    public float Power { get; }
    public float KnockForce { get; }
    public bool IsAttack { get; }
}
