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
        ChangeState(new ChasingState(this, target, map, this.gameObject));
        Debug.Log("Chasing");
    }

    protected override void StartWander()
    {
        ChangeState(new WanderState(gameObject,map));
    }

    protected override void StartMazeWalk()
    {
        ChangeState(new MazeWalkState(gameObject, map));
    }

    protected override void StartAttacking()
    {
        ChangeState(new MeleeAttackingState(gameObject, currentState, 5.0f));
    }

}
