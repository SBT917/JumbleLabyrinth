using Aoiti.Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TriggerEvent)), RequireComponent(typeof(Animator))]
public abstract class Enemy : MonoBehaviour
{

    public float health = 100f;
    public float speed = 5f;

    public int playerMaze;
    public enum State
    {
        Idle,
        Wander,
        Chasing,
        Attacking
    }

    protected State currentState = State.Idle;
    protected GameObject target;

    protected Animator animator;

    [SerializeField]
    private TriggerEvent triggerEvent;

    private void Start()
    {
        triggerEvent.onTriggerEnter += OnTargetEnter;
        triggerEvent.onTriggerExit += OnTargetExit;
        animator = GetComponent<Animator>();
        Initialize();
    }

    protected abstract void Initialize();

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            TeleportAndResetHealth();
        }
    }

    protected abstract void IdleBehavior();
    protected abstract void WanderBehavior();
    protected abstract void ChasingBehavior();
    protected abstract void AttackingBehavior();

    protected abstract void OnTargetEnter(Collider2D collision);
    protected abstract void OnTargetExit(Collider2D collision);
    private void TeleportAndResetHealth()
    {
        //敵のランダムな迷路にテレポート


        health = 100f;
    }

    protected void MoveTowardsTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }



    
}

