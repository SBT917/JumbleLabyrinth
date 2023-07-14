using Aoiti.Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ghost : Enemy
{
    private SpriteRenderer spriteRenderer;



    protected override void Initialize()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartWander();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            // Start coroutine to apply stun effect

        }
    }

    protected override void OnTargetEnter(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    target = collision.gameObject;
        //    StartChasing();
            
        //}
    }

    protected override void OnTargetExit(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    target = null;
        //    StartIdle();
        //}
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

    private void OnDrawGizmos()
    {
        if (currentState is ChasingState chasingState)
        {
            var path = chasingState.GetPathToDraw();
            if (path == null || path.Count == 0)
                return;

            Gizmos.color = Color.red;
            foreach (Vector2Int pos in path)
            {
                Gizmos.DrawSphere((Vector2)pos + new Vector2(0.5f, 0.5f), 0.1f);
            }
        }
    }
}
