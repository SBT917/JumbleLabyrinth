using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    public void Attack();
    public float GetPower();
    public void SetPower(float power);
}
