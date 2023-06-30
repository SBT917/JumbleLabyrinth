using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, IAttackable
{
    [SerializeField] PlayerStatus status;
    [SerializeField] Collider2D hitBox;

    float power; //UŒ‚—Í
    bool isAttacking = false; //UŒ‚ƒ‚[ƒVƒ‡ƒ“’†‚©‚Ç‚¤‚©

    void Awake()
    {
        power = status.defaultpower;
    }

    public float GetPower()
    {
        return power;
    }

    public void SetPower(float power)
    {
        this.power = power;
    }

    public void Attack()
    {
        if (isAttacking) return;
        StartCoroutine(AttackCroutine());
    }

    IEnumerator AttackCroutine()
    {
        isAttacking = true;
        Debug.Log("Attack");

        hitBox.enabled = true;
        yield return null;
        hitBox.enabled = false;

        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
    }
}
