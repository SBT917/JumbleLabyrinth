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
            if (path == null)
            {
                yield return new WaitForFixedUpdate();
            }
            foreach (Vector2Int position in path)
            {
                // Calculate a target position that is slightly inside the real target cell
                Vector2 targetPosition = (Vector2)position + new Vector2(0.5f, 0.5f); // Adjust this buffer as needed

                while (Vector2.Distance((Vector2)enemy.transform.position, targetPosition) > 0.05f)
                {
                    Vector2 newPosition = Vector2.MoveTowards(enemy.transform.position, targetPosition, enemy.GetComponent<Enemy>().speed * Time.deltaTime);
                    Vector2 direction = newPosition - (Vector2)enemy.transform.position;

                    enemy.transform.position = newPosition;
                    yield return new WaitForFixedUpdate();
                }
            }
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
    private Vector2Int currentDirection;
    private Vector3 targetPosition;

    public MazeWalkState(GameObject enemy, Tilemap map)
    {
        this.enemy = enemy;
        this.map = map;
        targetPosition = enemy.transform.position;

        // Choose a random initial direction
        List<Vector2Int> directions = new List<Vector2Int> { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        currentDirection = directions[UnityEngine.Random.Range(0, directions.Count)];
    }

    public void EnterState(Enemy enemy)
    {

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
        if ((Vector3)enemy.transform.position == targetPosition)
        {
            Vector3Int cellPosition = map.WorldToCell(enemy.transform.position);
            Vector2Int position = new Vector2Int(cellPosition.x, cellPosition.y);

            Vector2Int left = TurnLeft(currentDirection);

            if (!IsWall(position + left))  // If there's no wall to the left
            {
                currentDirection = left;  // Turn left
            }
            else if (IsWall(position + currentDirection))  // If there's a wall straight ahead
            {
                currentDirection = !IsWall(position + TurnRight(currentDirection)) ? TurnRight(currentDirection) : TurnAround(currentDirection);
            }

            targetPosition = map.GetCellCenterWorld(cellPosition + (Vector3Int)currentDirection);
        }

        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, targetPosition, enemy.GetComponent<Enemy>().speed * Time.deltaTime);
    }

    private Vector2Int TurnLeft(Vector2Int direction)
    {
        return new Vector2Int(-direction.y, direction.x);
    }

    private Vector2Int TurnRight(Vector2Int direction)
    {
        return new Vector2Int(direction.y, -direction.x);
    }

    private Vector2Int TurnAround(Vector2Int direction)
    {
        return new Vector2Int(-direction.x, -direction.y);
    }

    private bool IsWall(Vector2Int position)
    {
        return map.HasTile((Vector3Int)position);
    }
}

public class KnockbackState : IEnemyState
{
    private GameObject enemy;
    private Vector3 direction;
    private float speed;
    private float knockbackTime;
    private float timer;

    public KnockbackState(GameObject enemy, Vector3 direction, float speed, float knockbackTime)
    {
        this.enemy = enemy;
        this.direction = direction;
        this.speed = speed;
        this.knockbackTime = knockbackTime;
    }

    public void EnterState()
    {
        timer = 0;
    }

    public void UpdateState()
    {
        // Move enemy in the knockback direction
        enemy.transform.position += direction * speed * Time.deltaTime;

        // Increment timer
        timer += Time.deltaTime;

        // Check if knockback time has ended
        if (timer >= knockbackTime)
        {
            enemy.GetComponent<Enemy>().ChangeState(new IdleState());
        }
    }

    public void ExitState()
    {

    }

    public void EnterState(Enemy enemy)
    {
        
    }

    public List<Vector2Int> GetPathToDraw()
    {
        throw new NotImplementedException();
    }

    // Other methods...
}


#endregion
