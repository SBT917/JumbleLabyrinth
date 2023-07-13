using Aoiti.Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ghost : Enemy
{
    public float trackingDistance = 10f;
    public float stunDuration = 3f;
    private SpriteRenderer spriteRenderer;

    // 経路探索の間隔（秒）
    public float pathfindingInterval = 1f;

    // 次に経路探索を行う時刻
    private float nextPathfindingTime = 0f;

    private List<Vector2Int> pathToDraw;

    public Tilemap map;

    Vector2Int currentPatrolPoint;

    Coroutine followPathCoroutine;

    protected override void Initialize()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Implement random wandering code here.
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                IdleBehavior();
                break;
            case State.Wander:
                WanderBehavior();
                break;
            case State.Chasing:
                ChasingBehavior();
                break;
            case State.Attacking:
                AttackingBehavior();
                break;
        }

        Debug.Log(currentState);
    }

    protected override void IdleBehavior()
    {

    }
    protected override void WanderBehavior()
    {
        // Check if the enemy has reached its patrol point
        if (Vector2Int.FloorToInt(transform.position) == currentPatrolPoint || currentPatrolPoint == default(Vector2Int))
        {
            // If the enemy has reached its patrol point, get a new one
            currentPatrolPoint = GetRandomPatrolPoint();

            // Start pathfinding to the new patrol point
            Pathfinding(currentPatrolPoint);
        }
    }

    private Vector2Int GetRandomPatrolPoint()
    {
        var freeTiles = GetFreeTile.instance.GetFreeTiles();
        if (freeTiles.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, freeTiles.Count);
            Vector3Int randomPosition = freeTiles[randomIndex];
            Vector3 worldPosition = map.GetCellCenterWorld(randomPosition);
            return new Vector2Int((int)worldPosition.x, (int)worldPosition.y);
        }
        else
        {
            return Vector2Int.zero; // Or any other 'error' value
        }
    }


    protected override void ChasingBehavior()
    {
        //ターゲットがnullならWanderに戻る
        if(target == null)
        {
            currentState = State.Wander;
            return;
        }

        if (Time.time >= nextPathfindingTime)
        {
            Vector2Int goal = Vector2Int.FloorToInt(target.transform.position);
            Pathfinding(goal);
            nextPathfindingTime = Time.time + pathfindingInterval;
        }
    }



    protected override void AttackingBehavior()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currentState = State.Attacking;
            // Start coroutine to apply stun effect
            
        }
    }

    protected override void OnTargetEnter(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target = collision.gameObject;
            currentState = State.Chasing;
        }
    }

    protected override void OnTargetExit(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target = null;
            currentState = State.Wander;
        }
    }



    private bool IsWalkable(Vector2Int position)
    {
        // Tilemapから指定された位置のタイルを取得
        TileBase tile = map.GetTile((Vector3Int)position);

        // タイルが存在しなければ移動可能
        // タイルが存在する場合は移動不可能
        return tile == null;
    }

    #region Pathfinding
    void Pathfinding(Vector2Int goal)
    {
        Debug.Log("パスファインディング開始");
        // ユークリッド距離を計算するHeuristic関数
        Func<Vector2Int, Vector2Int, float> HeuristicFunction = (node1, node2) =>
        {
            return Vector2Int.Distance(node1, node2);
        };

        // 接続ノードを取得するConnectedNodes関数
        Func<Vector2Int, Dictionary<Vector2Int, float>> ConnectedNodesFunction = (node) =>
        {
            var result = new Dictionary<Vector2Int, float>();

            // 上下左右のセルを調べる
            var directions = new Vector2Int[]
            {
        new Vector2Int(0, 1),
        new Vector2Int(0, -1),
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0)
            };
            foreach (var direction in directions)
            {
                var nextNode = node + direction;
                if (IsWalkable(nextNode))
                {
                    result[nextNode] = 1; // 移動コストは一律1とする
                }
            }

            return result;
        };

        // パスファインダーのインスタンスを作成
        var pathfinder = new Pathfinder<Vector2Int>(HeuristicFunction, ConnectedNodesFunction);

        // 敵キャラクターの位置とプレイヤーの位置をセル座標に変換
        Vector2Int start = Vector2Int.FloorToInt(transform.position);
        //Vector2Int goal = Vector2Int.FloorToInt(target.transform.position);

        // 経路探索を実行
        bool pathFound = pathfinder.GenerateAstarPath(start, goal, out List<Vector2Int> path);



        // 経路が見つかった場合、敵キャラクターはその経路に沿って移動
        if (pathFound)
        {
            Debug.Log("Path found!");
            pathToDraw = path;
            // 前のコルーチンがあればそれを停止する
            if (followPathCoroutine != null)
            {
                StopCoroutine(followPathCoroutine);
            }
            // 新しいコルーチンを開始する
            followPathCoroutine = StartCoroutine(FollowPath(path));
        }

        // 経路に沿って移動するコルーチン
        IEnumerator FollowPath(List<Vector2Int> path)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            foreach (Vector2Int position in path)
            {
                // Calculate a target position that is slightly inside the real target cell
                Vector2 targetPosition = (Vector2)position + new Vector2(0.5f, 0.5f); // Adjust this buffer as needed

                while (Vector2.Distance((Vector2)transform.position, targetPosition) > 0.05f)
                {
                    Vector2 newPosition = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                    Vector2 direction = newPosition - (Vector2)transform.position;
                    direction.Normalize();

                    animator.SetFloat("MoveX", direction.x);
                    animator.SetFloat("MoveY", direction.y);

                    transform.position = newPosition;
                    yield return new WaitForFixedUpdate();
                }
            }
        }

    }
    #endregion

}

