using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeGen : MonoBehaviour
{
    [SerializeField]
    [Tooltip("迷宮を入れて")]
    private Tilemap maze, walls;
    [SerializeField]
    [Tooltip("タイルパレットからタイルを入れて")]
    private TileBase floorTile, wallTile, finishTile, spawnTile;
    [SerializeField]
    [Tooltip("迷宮の広さと高さを入れて")]
    private int i_max_x, i_max_y;

    private int[,] i_map;

    private const int Floor = 0, Wall = 9;

    private Vector3Int finishLocation, spawnLocation;

    void Awake()
    {
        //ゴールとスポーン地点を設定する
        finishLocation = new Vector3Int(Random.Range(3, i_max_x), Random.Range(3, i_max_y), 0);
        spawnLocation = new Vector3Int(i_max_x / 2, i_max_y / 2, 0);

        // 迷路を生成する
        GenerateMaze();
    }

    private void GenerateMaze()
    {
        int i_temp;

        // 迷路の地図を初期化する
        i_map = new int[i_max_x + 1, i_max_y + 1];

        // 外壁を埋める
        FillOuterWalls();

        i_temp = 4;

        // 迷路の内部を生成する
        for (int y = 3; y <= i_max_y - 1; y += 2)
        {
            if (y != 3)
            {
                i_temp = 3;
            }

            for (int x = 3; x <= i_max_x - 1; x += 2)
            {
                // 床を設定する
                i_map[x, y] = 9;

                // ランダムに壁を設定する
                switch (Random.Range(Floor, i_temp))
                {
                    case 0:
                        i_map[x + 1, y] = Wall;
                        break;
                    case 1:
                        i_map[x - 1, y] = Wall;
                        break;
                    case 2:
                        i_map[x, y + 1] = Wall;
                        break;
                    case 3:
                        if (y == 3)
                        {
                            i_map[x, y - 1] = Wall;
                        }
                        break;
                }
            }

            // 迷路のタイルを埋める
            FillMazeTiles();
        }
    }

    private void FillOuterWalls()
    {
        // 上下の外壁を埋める
        for (int i = 1; i <= i_max_x; i++)
        {
            i_map[i, 1] = Wall;
            i_map[i, i_max_y] = Wall;
        }

        // 左右の外壁を埋める
        for (int i = 1; i <= i_max_y; i++)
        {
            i_map[1, i] = Wall;
            i_map[i_max_x, i] = Wall;
        }
    }

    private void FillMazeTiles()
    {
        // 迷路のタイルを埋める
        for (int x = 1; x <= i_max_x; x++)
        {
            for (int y = 1; y <= i_max_y; y++)
            {
                if (i_map[x, y] == Floor)
                {
                    // 床のタイルを設定する
                    if (new Vector3Int(x, y, 0) == finishLocation)
                    {
                        maze.SetTile(new Vector3Int(x - (i_max_x / 2), y - (i_max_y / 2), 0), finishTile);
                    }
                    else if (new Vector3Int(x, y, 0) == spawnLocation)
                    {
                        maze.SetTile(new Vector3Int(x - (i_max_x / 2), y - (i_max_y / 2), 0), spawnTile);
                    }
                    else
                    {
                        maze.SetTile(new Vector3Int(x - (i_max_x / 2), y - (i_max_y / 2), 0), floorTile);
                    }
                }
                else if (i_map[x, y] == Wall)
                {
                    // 壁のタイルを設定する
                    if (finishLocation == new Vector3Int(x, y, 0) || spawnLocation == new Vector3Int(x, y, 0))
                    {
                        continue;
                    }
                    walls.SetTile(new Vector3Int(x - (i_max_x / 2), y - (i_max_y / 2), 0), wallTile);
                }
            }
        }
    }
}
