using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, IAttackable
{
    [SerializeField] PlayerStatus status;
    [SerializeField] GameObject attackObject;

    Animator animator;

    float power; //�U����
    float attackSpeed; //�U�����x
    bool isAttacking = false; //�U�����[�V���������ǂ���

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

        Vector3 dir = Vector3.zero;
        if (animator.GetFloat("DirectionX") == 1) dir.x = 1;
        else if (animator.GetFloat("DirectionX") == -1) dir.x = -1;
        else dir.x = 0;

        if(animator.GetFloat("DirectionY") == 1) dir.y = 1;
        else if (animator.GetFloat("DirectionY") == -1) dir.y = -1;
        else dir.y = Mathf.RoundToInt(animator.GetFloat("DirectionY")); ;

        Vector3 pos = transform.position + dir;
        Debug.Log(dir);
        var o = Instantiate(attackObject, pos, Quaternion.identity, transform);
        yield return new WaitForSeconds(0.2f);
        Destroy(o);

        yield return new WaitForSeconds(attackSpeed);
        isAttacking = false;
    }
}
