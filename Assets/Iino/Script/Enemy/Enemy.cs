using Aoiti.Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Animator))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    public int EnemyID;

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

    [SerializeField]
    private GameObject originalEnemyPrefab;

    [SerializeField]
    private float itemDropRate;

    [SerializeField]
    private float coinDropRate;

    [SerializeField]
    private float redCoinDropRate;

    [SerializeField]
    private float enemySendRate;


    [SerializeField]
    private GameObject[] DropItems;

    [SerializeField]
    private GameObject[] coins;



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

    public virtual void TakeDamage(float damage, Transform attacker, float knockbackSpeed = 2, float knockbackTime = 0.5f)
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
    protected virtual void TeleportAndResetHealth()
    {
        CreateEnemyDestroyAnimation();
        DropItemLottery();

        //// プレイヤーIDを切り替える
        playerID = 1 - playerID;
        float randomValue = UnityEngine.Random.value;
        if (randomValue <= enemySendRate)
        {
            EnemySpawnManager.instance.RespawnEnemy(EnemyID, playerID, transform.position, EnemySpawnManager.instance.sendTrails[0]);
        }
        EnemySpawnManager.instance.RemoveEnemyCount(1);
        Destroy(gameObject);
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

    protected void CreateEnemyDestroyAnimation()
    {
        Instantiate(EnemyDestroyAnim, transform.position, Quaternion.identity);
    }

    private void DropItemLottery()
    {
        float randomValue = UnityEngine.Random.value; // 0.0から1.0までの乱数を取得
        if(randomValue <= redCoinDropRate) 
        {
            GenerateCoin(1);
            return;
        }
        else if(randomValue <= coinDropRate)
        {
            //コインをドロップ
            GenerateCoin(0);
            return;
        }

        randomValue = UnityEngine.Random.value; // 0.0から1.0までの乱数を取得
        if (randomValue <= itemDropRate)
        {
            // アイテムをドロップ
            GenerateItem();
        }
    }

    private void GenerateCoin(int coinID)
    {
        Instantiate(coins[coinID], transform.position, Quaternion.identity);
    }

    private void GenerateItem()
    {
        Instantiate(DropItems[UnityEngine.Random.Range(0, DropItems.Length)], transform.position, Quaternion.identity);
    }
}

