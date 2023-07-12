using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, IAttackable
{
    [SerializeField] PlayerStatus status;
    [SerializeField] GameObject attackObject;

    Animator animator;

    float power; //çUåÇóÕ
    float attackSpeed; //çUåÇë¨ìx
    bool isAttacking = false; //çUåÇÉÇÅ[ÉVÉáÉìíÜÇ©Ç«Ç§Ç©

    void Awake()
    {
        TryGetComponent(out animator);
        power = status.defaultpower;
        attackSpeed = status.defaultAttackSpeed;
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

        Vector3 pos = transform.position + new Vector3(animator.GetFloat("DirectionX"), animator.GetFloat("DirectionY"), 0);
        var o = Instantiate(attackObject, pos, Quaternion.identity, transform);
        yield return new WaitForSeconds(0.2f);
        Destroy(o);

        yield return new WaitForSeconds(attackSpeed);
        isAttacking = false;
    }
}
