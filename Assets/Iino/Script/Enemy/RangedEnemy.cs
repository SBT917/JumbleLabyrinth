using Aoiti.Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RangedEnemy : Enemy
{
    [SerializeField] 
    private GameObject arrowPrefab;


    private RangedEnemyChasingState chasingState;

    protected override void Initialize()
    {
        StartMazeWalk();
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
        //ChasingStateを割り当て、デリゲートにStartAttackingを割り当てる
        chasingState = new RangedEnemyChasingState(this, target, map, this.gameObject);
        chasingState.OnRaycastHit += StartAttacking;
        ChangeState(chasingState);
    }

    protected override void StartWander()
    {
        ChangeState(new WanderState(gameObject, map));
    }

    protected override void StartMazeWalk()
    {
        ChangeState(new MazeWalkState(gameObject, map));
    }

    protected override void StartAttacking()
    {
        ChangeState(new RangedAttackingState(gameObject, currentState, 1.0f, arrowPrefab, chasingState.direction));
    }

}
