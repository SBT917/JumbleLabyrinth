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

    public Tilemap map;

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

    public void TakeDamage(float damage, Transform attacker,float knockbackSpeed = 5, float knockbackTime = 1)
    {
        health -= damage;
        //ノックバックする方向を決定
        Vector2 knockbackDirection = (transform.position - attacker.position).normalized;

        ChangeState(new KnockbackState(gameObject, knockbackDirection, knockbackSpeed, knockbackTime));
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

    protected virtual void StartWander()
    {
        //ChangeState(new WanderState());
    }

    protected virtual void StartChasing()
    {
        //ChangeState(new ChasingState());
    }

    protected virtual void StartMazeWalk()
    {

    }

    protected void StartAttacking()
    {
        //ChangeState(new AttackingState());
    }

    protected virtual void StartKnokback()
    {
        //ChangeState(new KnockbackState());
    }

    protected abstract void OnTargetEnter(Collider2D collision);
    protected abstract void OnTargetExit(Collider2D collision);
    private void TeleportAndResetHealth()
    {
        //敵のランダムな迷路にテレポート


        health = maxHealth;
    }



}

