using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, IAttackable
{
    [SerializeField] PlayerStatus status;
    [SerializeField] GameObject attackObject;

    Animator animator;

    bool enable;
    float power; //攻撃力
    float knockForce; //ノックバック力
    float attackSpeed; //攻撃速度
    bool isAttacking = false; //攻撃中かどうか

    public bool Enable { get => enable; set => enable = value; }
    public float Power { get => power; }
    public float KnockForce { get => knockForce; }
    public bool IsAttack { get => isAttacking; }

    void Awake()
    {
        TryGetComponent(out animator);
        enable = true;
        power = status.defaultpower;
        knockForce = status.defaultKnockForce;
        attackSpeed = status.defaultAttackSpeed;
    }

    public void Attack()
    {
        if (!enable) return;
        if (isAttacking) return;

        StartCoroutine(AttackCroutine());
        AudioManager.instance.PlaySE("Attack");
    }

    IEnumerator AttackCroutine()
    {
        isAttacking = true;
        Debug.Log("Attack");

        Vector3 dir = Vector3.zero;

        //X方向の攻撃位置をセット
        dir.x = Mathf.RoundToInt(animator.GetFloat("DirectionX"));

        //Y方向の攻撃位置をセット
        dir.y = Mathf.RoundToInt(animator.GetFloat("DirectionY"));

        //斜め向きの場合はX方向の攻撃位置を0にする
        if(dir.x != 0 && dir.y != 0)
        {
            dir.x = 0.0f;
        }

        //攻撃判定を出現させる
        Vector3 pos = transform.position + dir;
        var o = Instantiate(attackObject, pos, Quaternion.identity, transform);
        yield return new WaitForSeconds(0.2f);
        Destroy(o);

        yield return new WaitForSeconds(attackSpeed);
        isAttacking = false;
    }
}
