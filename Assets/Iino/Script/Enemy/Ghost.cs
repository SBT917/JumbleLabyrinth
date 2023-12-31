using Aoiti.Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ghost : Enemy
{
    protected override void Initialize()
    {
        
        StartMazeWalk();
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
                coinCollecter.LoseCoin(1);
            }
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
        ChangeState(new MeleeAttackingState(gameObject, currentState, 2.0f));
    }

}
