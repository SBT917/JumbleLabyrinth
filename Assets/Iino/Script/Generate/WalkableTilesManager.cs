using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WalkableTilesManager : MonoBehaviour
{
    // シングルトン
    public static WalkableTilesManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        freeTiles = new List<Vector3Int>[tilemaps.Length];
        for (int i = 0; i < tilemaps.Length; i++)
        {
            UpdateFreeTiles(i);
        }
    }

    // タイルマップ
    [SerializeField]
    private Tilemap[] tilemaps;

    // 空きタイルのリスト
    private List<Vector3Int>[] freeTiles;

    private void Start()
    {

    }

    // 空きタイルのリストを更新
    public void UpdateFreeTiles(int playerIndex)
    {
        var tilemap = tilemaps[playerIndex];
        var bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        freeTiles[playerIndex] = new List<Vector3Int>();

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int tileLocation = new Vector3Int(x, y, 0); // 2Dタイルマップなのでz=0

                if (tilemap.GetTile(tileLocation) == null)
                {
                    freeTiles[playerIndex].Add(tileLocation);
                }
            }
        }
    }

    // 歩行可能タイルのリストを返す
    public List<Vector3Int> GetFreeTiles(int playerIndex)
    {
        return freeTiles[playerIndex];
    }

    /// <summary>
    /// ランダムな歩行可能タイルを返す
    /// </summary>
    /// <param name="playerIndex"></param>
    /// <returns></returns>
    public Vector3? GetRandomPoint(int playerIndex)
    {
        if (freeTiles[playerIndex].Count == 0)
        {
            Debug.LogError("No free tiles available!");
            return null;
        }

        int randomIndex = UnityEngine.Random.Range(0, freeTiles[playerIndex].Count);
        Vector3Int localPosition = freeTiles[playerIndex][randomIndex];

        // Cell の中心にオフセットを追加
        Vector3 centerOffset = new Vector3(0f, 0f, 0);
        Vector3 worldPosition = tilemaps[playerIndex].CellToWorld(localPosition) + centerOffset;

        return worldPosition;
    }


    /// <summary>
    /// プレイヤーの近くにあるランダムな歩行可能タイルを返す
    /// </summary>
    /// <param name="playerIndex"></param>
    /// <param name="playerPosition"></param>
    /// <param name="maxDistance"></param>
    /// <returns></returns>
    public Vector3? GetRandomPointNearPlayer(int playerIndex, Vector3 playerPosition, float maxDistance,float minDistance)
    {
        // プレイヤーの近くの歩行可能なタイルを格納するリスト
        List<Vector3Int> tilesNearPlayer = new List<Vector3Int>();

        foreach (Vector3Int tile in freeTiles[playerIndex])
        {
            // タイルのワールド座標を取得
            Vector3 worldPosition = tilemaps[playerIndex].CellToWorld(tile);

            // プレイヤーとタイルの距離を計算
            float distanceToPlayer = Vector3.Distance(playerPosition, worldPosition);

            // タイルがプレイヤーの近くにあるか判定
            if (distanceToPlayer <= maxDistance && distanceToPlayer >= minDistance)
            {
                tilesNearPlayer.Add(tile);
            }
        }

        // 近くのタイルが存在しない場合
        if (tilesNearPlayer.Count == 0)
        {
            Debug.LogError("指定された座標の近くに空きタイルが存在しない");
            return GetRandomPoint(playerIndex);
        }

        // 近くのタイルの中からランダムに選ぶ
        int randomIndex = UnityEngine.Random.Range(0, tilesNearPlayer.Count);
        Vector3Int localPosition = tilesNearPlayer[randomIndex];

        // Cell の中心にオフセットを追加
        Vector3 centerOffset = new Vector3(0.5f, 0.5f, 0);
        Vector3 worldPositionNearPlayer = tilemaps[playerIndex].CellToWorld(localPosition) + centerOffset;

        Debug.Log("リスポーン場所：" + worldPositionNearPlayer.ToString());
        tilesNearPlayer.Clear();
        return worldPositionNearPlayer;
    }
}

