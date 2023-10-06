using Aoiti.Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemSpawnEnemy : Enemy
{
    [SerializeField]
    private float DisappearTime;

    protected override void Initialize()
    {
        StartMazeWalk();
        Invoke(((Action)this.TeleportAndResetHealth).Method.Name, DisappearTime);
        Debug.Log("init");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartAttacking();

            collision.transform.TryGetComponent(out IInvisiblable invisiblable);
            if (invisiblable.IsInvisible) return;

            if (collision.transform.TryGetComponent(out IKnockBackable knockBackable))
            {
                Vector2 dir = collision.transform.position - transform.position;
                knockBackable.StartKnockBack(dir, 2f, 0.1f);
            }

            if (collision.transform.TryGetComponent(out IStanable stanable))
            {
                stanable.StartStan(1f);
            }

            if (collision.transform.TryGetComponent(out ICoinCollecter coinCollecter))
            {
                coinCollecter.LoseCoin(3);
            }
            TeleportAndResetHealth();
        }
    }

    protected override void OnTargetEnter(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target = collision.gameObject;
            StartChasing();
        }
    }

    protected override void OnTargetExit(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target = null;
            StartMazeWalk();
        }
    }

    protected override void StartChasing()
    {
        ChangeState(new ChasingState(this, target, maps[playerID], this.gameObject));
    }

    protected override void StartMazeWalk()
    {
        if (playerID >= 0 && playerID < maps.Count)
        {
            ChangeState(new MazeWalkState(gameObject, maps[playerID]));
        }
        else
        {
            Debug.LogError("Invalid playerID or maps count" + "playerID:" + playerID + "maps.Count" + maps.Count);
        }
    }

    protected override void StartAttacking()
    {
        ChangeState(new MeleeAttackingState(gameObject, currentState, 5.0f));
    }

    public override void TakeDamage(float damage, Transform attacker, float knockbackSpeed = 2, float knockbackTime = 0.5F)
    {
        //base.TakeDamage(damage, attacker, knockbackSpeed, knockbackTime);
        //”íUŒ‚Žž‚Ìˆ—‚ð–³Ž‹‚·‚é
    }

    protected override void TeleportAndResetHealth()
    {
        //‚±‚Ì“G‚Í“|‚³‚ê‚½ŽžA‘ŠŽè‚É‘—‚ç‚¸Á–Å‚·‚é
        CreateEnemyDestroyAnimation();
        Destroy(gameObject);

    }

}
