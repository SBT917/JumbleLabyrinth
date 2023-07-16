using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    public void Attack();
    public float Power { get; }
    public bool IsAttack { get; }
}
