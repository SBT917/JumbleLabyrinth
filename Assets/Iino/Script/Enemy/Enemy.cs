using Aoiti.Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Animator))]
public abstract class Enemy : MonoBehaviour
{
    [NonSerialized]
    public float health;
    public float maxHealth = 100f;
    public float speed = 5f;

    public int firstGenetrateNum;

    [NonSerialized]
    public int playerID;

    protected GameObject target;

    protected Animator animator;

    [NonSerialized]
    public List<Tilemap> maps = new List<Tilemap>();

    [SerializeField]
    private TriggerEvent triggerEvent;

    [SerializeField]
    private GameObject EnemySpawnAnim;

    [SerializeField]
    private GameObject EnemyDestroyAnim;

    private void Start()
    {
        CreateEnemySpawnAnimation();
        triggerEvent.onTriggerEnter += OnTargetEnter;
        triggerEvent.onTriggerExit += OnTargetExit;
        animator = GetComponent<Animator>();
        health = maxHealth;


        maps.Add(GameObject.Find("Grid1/walls1").GetComponent<Tilemap>());
        maps.Add(GameObject.Find("Grid2/walls2").GetComponent<Tilemap>());
        Initialize();


    }


    protected abstract void Initialize();

    public void TakeDamage(float damage, Transform attacker, float knockbackSpeed = 5, float knockbackTime = 1)
    {
        health -= damage;
        //ノックバックする方向を決定
        Vector2 knockbackDirection = (transform.position - attacker.position).normalized;

        ChangeState(new KnockbackState(gameObject,currentState, knockbackDirection, knockbackSpeed, knockbackTime));
        if (health <= 0)
        {
            TeleportAndResetHealth();
        }
    }

    protected IEnemyState currentState;

    private void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState();
        }
        else
        {
            Debug.LogError("currentState is null");
        }
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

    protected virtual void StartChasing()
    {
        //ChangeState(new ChasingState());
    }

    protected virtual void StartMazeWalk()
    {

    }

    protected virtual void StartAttacking()
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
        CreateEnemyDestroyAnimation();

        //// プレイヤーIDを切り替える
        playerID = 1 - playerID;

        //// 敵の近くに空きがない場合、相手のマップにテレポートする
        var randomPosition = WalkableTilesManager.instance.GetRandomPoint(playerID);

        if (!randomPosition.HasValue) // null check
        {
            Debug.LogError($"No free tiles available for player {playerID}.");
            return;
        }
        var enemy = Instantiate(this, randomPosition.Value, Quaternion.identity);
        enemy.playerID = playerID;
        Destroy(gameObject);
        //Debug.Log($"Teleported to {transform.position}.");
    }

    private void SetAnimatorParameters(Vector2 direction)
    {
        animator.SetFloat("moveX", direction.x);
        animator.SetFloat("moveY", direction.y);
    }


    private void CreateEnemySpawnAnimation()
    {
        Instantiate(EnemySpawnAnim,transform.position,Quaternion.identity);
    }

    private void CreateEnemyDestroyAnimation()
    {
        Instantiate(EnemyDestroyAnim, transform.position, Quaternion.identity);
    }
}

