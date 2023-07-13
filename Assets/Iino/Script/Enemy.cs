using Aoiti.Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Animator))]
public abstract class Enemy : MonoBehaviour
{

    public float health;
    public float maxHealth = 100f;
    public float speed = 5f;

    public int playerMaze;
    protected GameObject target;

    protected Animator animator;

    [SerializeField]
    private TriggerEvent triggerEvent;

    private void Start()
    {
        triggerEvent.onTriggerEnter += OnTargetEnter;
        triggerEvent.onTriggerExit += OnTargetExit;
        animator = GetComponent<Animator>();
        health = maxHealth;
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

    protected IEnemyState currentState;

    private void Update()
    {
        currentState.UpdateState();
    }

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }

        currentState = newState;
        currentState.EnterState(this);
    }

    protected void StartIdle()
    {
        ChangeState(new IdleState());
    }

    protected void StartWander()
    {
        ChangeState(new WanderState());
    }

    protected void StartChasing()
    {
        ChangeState(new ChasingState());
    }

    protected void StartAttacking()
    {
        ChangeState(new AttackingState());
    }

    protected abstract void OnTargetEnter(Collider2D collision);
    protected abstract void OnTargetExit(Collider2D collision);
    private void TeleportAndResetHealth()
    {
        //敵のランダムな迷路にテレポート


        health = maxHealth;
    }




    
}

