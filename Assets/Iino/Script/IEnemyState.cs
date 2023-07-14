using Aoiti.Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public interface IEnemyState
{
    void EnterState(Enemy enemy);
    void UpdateState();
    void ExitState();

    List<Vector2Int> GetPathToDraw();
}
#region Enemy States
public class IdleState : IEnemyState
{
    //...state specific methods
    public void EnterState(Enemy enemy)
    {
        Debug.Log("Idle");
    }

    public void ExitState()
    {

    }

    public List<Vector2Int> GetPathToDraw()
    {
        throw new NotImplementedException();
    }

    public void UpdateState()
    {

    }
}

public class WanderState : IEnemyState
{
    private GameObject enemy;

    Vector2[] directions = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    Vector2 currentDirection;

    Tilemap map;
    public WanderState(GameObject enemy, Tilemap map)
    {
        this.enemy = enemy;
        this.map = map;
    }

    //...state specific methods
    public void EnterState(Enemy enemy)
    {
        ChooseRandomDirection();
    }

    public void ExitState()
    {

    }

    public void UpdateState()
    {
        Vector2Int nextGridPos = Vector2Int.RoundToInt((Vector2)enemy.transform.position + currentDirection);
        Vector3 nextGridPosVec3 = new Vector3(nextGridPos.x, nextGridPos.y, 0);

        // Draw debug line
        Debug.DrawLine(enemy.transform.position, nextGridPosVec3, Color.red, 2f);

        // If the next position is not walkable or is at the corner of a tile, choose a new direction
        if (!IsWalkable(nextGridPos) || IsAtTileCorner(nextGridPos))
        {
            ChooseRandomDirection();
            return;
        }

        enemy.transform.position = (Vector2)enemy.transform.position + currentDirection * enemy.GetComponent<Enemy>().speed * Time.deltaTime;
    }

    bool IsAtTileCorner(Vector2Int position)
    {
        float buffer = 0.1f; // Adjust this buffer as needed
        Vector2 currentPosition = (Vector2)enemy.transform.position;
        Vector2Int currentGridPos = Vector2Int.RoundToInt(currentPosition);

        // Check if the enemy is at the corner of a tile
        return (Mathf.Abs(currentPosition.x - currentGridPos.x) <= buffer && Mathf.Abs(currentPosition.y - currentGridPos.y) <= buffer);
    }


    void ChooseRandomDirection()
    {
        Vector2 newDirection = directions[UnityEngine.Random.Range(0, directions.Length)];
        Vector2Int newGridPos = Vector2Int.RoundToInt((Vector2)enemy.transform.position + newDirection);

        // If the new direction is not walkable, try again
        if (!IsWalkable(newGridPos))
        {
            ChooseRandomDirection();
            return;
        }

        currentDirection = newDirection;
    }


    bool IsWalkable(Vector2Int position)
    {
        // Tilemapから指定された位置のタイルを取得
        TileBase tile = map.GetTile((Vector3Int)position);

        // タイルが存在しなければ移動可能
        // タイルが存在する場合は移動不可能
        return tile == null;
    }

    public List<Vector2Int> GetPathToDraw()
    {
        throw new NotImplementedException();
    }
}



public class ChasingState : IEnemyState
{
    // 経路探索の間隔（秒）
    public float pathfindingInterval = 1f;

    // 次に経路探索を行う時刻
    private float nextPathfindingTime = 0f;

    private MonoBehaviour monoBehaviour;

    Coroutine followPathCoroutine;

    private Tilemap map;

    private GameObject target;

    private GameObject enemy;

    public List<Vector2Int> pathToDraw = null;

    public ChasingState(MonoBehaviour monoBehaviour, GameObject target, Tilemap map, GameObject enemy)
    {
        this.monoBehaviour = monoBehaviour;
        this.target = target;
        this.map = map;
        this.enemy = enemy;
    }

    public void EnterState(Enemy enemy)
    {

    }

    public void ExitState()
    {
        if (followPathCoroutine != null)
        {
            monoBehaviour.StopCoroutine(followPathCoroutine);
        }
    }

    public void UpdateState()
    {
        if (Time.time >= nextPathfindingTime)
        {
            Vector2Int goal = Vector2Int.FloorToInt(target.transform.position);
            Pathfinding(goal);
            nextPathfindingTime = Time.time + pathfindingInterval;
        }
    }


    void Pathfinding(Vector2Int goal)
    {
        
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
        Vector2Int start = Vector2Int.FloorToInt(enemy.transform.position);
        //Vector2Int goal = Vector2Int.FloorToInt(target.transform.position);

        // 経路探索を実行
        bool pathFound = pathfinder.GenerateAstarPath(start, goal, out List<Vector2Int> path);


        // 経路が見つかった場合、敵キャラクターはその経路に沿って移動
        if (pathFound)
        {
            pathToDraw = path;

            // 前のコルーチンがあればそれを停止する
            if (followPathCoroutine != null)
            {
                monoBehaviour.StopCoroutine(followPathCoroutine);
            }
            // 新しいコルーチンを開始する
            followPathCoroutine = monoBehaviour.StartCoroutine(FollowPath(path));
        }

        bool IsWalkable(Vector2Int position)
        {
            // Tilemapから指定された位置のタイルを取得
            TileBase tile = map.GetTile((Vector3Int)position);

            // タイルが存在しなければ移動可能
            // タイルが存在する場合は移動不可能
            return tile == null;
        }

        IEnumerator FollowPath(List<Vector2Int> path)
        {

            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            if(path==null)
            {
                yield return new WaitForFixedUpdate();
            }
            foreach (Vector2Int position in path)
            {
                // Calculate a target position that is slightly inside the real target cell
                Vector2 targetPosition = (Vector2)position + new Vector2(0.5f, 0.5f); // Adjust this buffer as needed

                while (Vector2.Distance((Vector2)enemy.transform.position, targetPosition) > 0.05f)
                {


                    Debug.Log("Current Position: " + enemy.transform.position + ", Target Position: " + targetPosition);

                    Vector2 newPosition = Vector2.MoveTowards(enemy.transform.position, targetPosition, enemy.GetComponent<Enemy>().speed * Time.deltaTime);
                    Vector2 direction = newPosition - (Vector2)enemy.transform.position;

                    enemy.transform.position = newPosition;
                    yield return new WaitForFixedUpdate();
                }
            }

            Debug.Log("FollowPath finished");
        }
    }

    public List<Vector2Int> GetPathToDraw()
    {
        return pathToDraw;
    }
}

public class AttackingState : IEnemyState
{
    //...state specific methods
    public void EnterState(Enemy enemy)
    {
        throw new System.NotImplementedException();
    }

    public void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateState()
    {
        throw new System.NotImplementedException();
    }

    public List<Vector2Int> GetPathToDraw()
    {
        throw new NotImplementedException();
    }
}

public class MazeWalkState : IEnemyState
{
    private GameObject enemy;
    private Tilemap map;
    private Vector2Int currentCell;
    private Vector2Int lastCell;
    private Vector2Int currentDirection;

    public MazeWalkState(GameObject enemy, Tilemap map)
    {
        this.enemy = enemy;
        this.map = map;
    }

    public void EnterState(Enemy enemy)
    {
        currentCell = GetCellPosition(enemy.transform.position);
        lastCell = currentCell;
        currentDirection = Vector2Int.down; // スタート地点から下方向へ移動開始
    }

    public void ExitState()
    {
    }

    public void UpdateState()
    {
        // 現在のセル位置を更新
        currentCell = GetCellPosition(enemy.transform.position);

        // もし前回のセルと現在のセルが異なっていれば、移動方向を更新
        if (currentCell != lastCell)
        {
            UpdateDirection();
            lastCell = currentCell;
        }

        // 移動処理
        Vector2 newPosition = (Vector2)enemy.transform.position + (Vector2)currentDirection * enemy.GetComponent<Enemy>().speed * Time.deltaTime;
        enemy.transform.position = newPosition;
    }

    void UpdateDirection()
    {
        // 左手法に基づいて次の移動方向を決定
        Vector2Int[] directions = new Vector2Int[]
        {
            currentDirection,
            RotateDirection(currentDirection, -90), // 左折
            RotateDirection(currentDirection, 90),  // 右折
            RotateDirection(currentDirection, 180)  // 後退
        };

        foreach (Vector2Int direction in directions)
        {
            Vector2Int nextCell = currentCell + direction;
            if (IsWalkable(nextCell))
            {
                currentDirection = direction;
                return;
            }
        }

        // 全ての方向が壁だった場合は180度回転して後退
        currentDirection = -currentDirection;
    }

    Vector2Int RotateDirection(Vector2Int direction, float angle)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        Vector2 rotatedDirection = rotation * new Vector2(direction.x, direction.y);
        return Vector2Int.RoundToInt(rotatedDirection);
    }

    bool IsWalkable(Vector2Int position)
    {
        // タイルマップから指定された位置のタイルを取得
        TileBase tile = map.GetTile((Vector3Int)position);

        // タイルが存在しなければ移動可能
        // タイルが存在する場合は移動不可能
        return tile == null;
    }

    Vector2Int GetCellPosition(Vector3 position)
    {
        return Vector2Int.RoundToInt(new Vector2(position.x, position.y));
    }

    public List<Vector2Int> GetPathToDraw()
    {
        throw new NotImplementedException();
    }
}




#endregion
