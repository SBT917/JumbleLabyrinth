using Aoiti.Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using static UnityEngine.EventSystems.EventTrigger;


public class EnemyTest : MonoBehaviour
{
    [SerializeField]
    private GameObject player;


    [SerializeField]
    private float moveSpeed;


    // 経路探索の間隔（秒）
    public float pathfindingInterval = 1f;

    // 次に経路探索を行う時刻
    private float nextPathfindingTime = 0f;


    public Tilemap map;  // これはUnityのInspectorから設定します

    Vector2Int previousPlayerPosition;
    float someThreshold = 1f;  // プレイヤーが1以上動いたら経路を再計算

    Coroutine followPathCoroutine; // 新たにコルーチンを追加

    void Update()
    {
        Vector2Int currentPlayerPosition = Vector2Int.FloorToInt(player.transform.position);
        if (Vector2Int.Distance(previousPlayerPosition, currentPlayerPosition) > someThreshold)
        {
            if (Time.time >= nextPathfindingTime)
            {
                Pathfinding();
                nextPathfindingTime = Time.time + pathfindingInterval;
            }
        }
        previousPlayerPosition = currentPlayerPosition;
    }


    private bool IsWalkable(Vector2Int position)
    {
        // Tilemapから指定された位置のタイルを取得
        TileBase tile = map.GetTile((Vector3Int)position);

        // タイルが存在しなければ移動可能（つまりtrueを返す）
        // タイルが存在する場合は移動不可能（つまりfalseを返す）
        return tile == null;
    }

    void Pathfinding()
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
                if (IsWalkable(nextNode)) // IsWalkableはそのセルが移動可能かをチェックする関数
                {
                    result[nextNode] = 1; // 移動コストは一律1とする
                }
            }

            return result;
        };

        // パスファインダーのインスタンスを作成
        var pathfinder = new Pathfinder<Vector2Int>(HeuristicFunction, ConnectedNodesFunction);

        // 敵キャラクターの位置とプレイヤーの位置をセル座標に変換します
        // これは実際のゲームのシチュエーションによって変わる可能性があります
        Vector2Int start = Vector2Int.FloorToInt(transform.position);
        Vector2Int goal = Vector2Int.FloorToInt(player.transform.position);

        // 経路探索を実行します
        bool pathFound = pathfinder.GenerateAstarPath(start, goal, out List<Vector2Int> path);

        // 経路が見つかった場合、敵キャラクターはその経路に沿って移動します
        if (pathFound)
        {
            Debug.Log("Path found!");
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
            Rigidbody2D rb = GetComponent<Rigidbody2D>();  // Rigidbody2Dコンポーネントを取得します

            foreach (Vector2Int position in path)
            {
                Vector2 targetPosition = (Vector2)position;
                while (Vector2.Distance((Vector2)transform.position, targetPosition) > 0.05f)  // 0.05は許容するエラーの範囲です
                {
                    Vector2 newPosition = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);  // 現在の位置から目標位置に向かって移動します
                    transform.position = newPosition;  // Transformの位置を更新します

                    yield return new WaitForFixedUpdate();  // FixedUpdate間隔で実行します（物理更新に合わせて）
                }
            }
        }

    }
}

