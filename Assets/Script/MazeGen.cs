
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class MazeGen : MonoBehaviour
{
    [SerializeField]
    private Tilemap maze;
    [SerializeField]
    private TileBase floorTile, wallTile, finishTile;
    [SerializeField]
    private int i_max_x, i_max_y;

    //çÏê¨ÇµÇΩñ¿òHÇÃäiî[ïœêî
    private int[,] i_map;

    private const int Floor = 0, Wall = 9, Finish = -1;


    private Vector3Int finishLocation;
    void Start()
    {
        finishLocation = new Vector3Int(Random.Range(3, i_max_x), Random.Range(3, i_max_y),0);
        Debug.Log(finishLocation);
        GenerateMaze();
    }
    private void GenerateMaze()
    {

        int i_temp;

        i_map = new int[i_max_x + 1, i_max_y + 1];

        FillOuterWalls();

        i_temp = 4;
        for (int y = 3; y <= i_max_y - 1; y += 2)
        {

            if (y != 3)
            {
                i_temp = 3;
            }

            for (int x = 3; x <= i_max_x - 1; x += 2)
            {
                i_map[x, y] = 9;
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
            FillMazeTiles();
        }
    }
    private void FillOuterWalls()
    {
        for (int i = 1; i <= i_max_x; i++)
        {
            i_map[i, 1] = Wall;
            i_map[i, i_max_y] = Wall;
        }

        for (int i = 1; i <= i_max_y; i++)
        {
            i_map[1, i] = Wall;
            i_map[i_max_x, i] = Wall;
        }
    }
    private void FillMazeTiles()
    {

        //É}ÉbÉvÇ…îΩâf ñ¿òHÇÃê^ÇÒíÜÇÉXÉ^Å[ÉgÇ…ÇµÇΩÇ¢ÇÃÇ≈ÅAîΩâfèÍèäÇîºï™Ç∏ÇÁÇµÇƒÇ¢ÇÈ 0í âﬂ 9ï«
        for (int x = 1; x <= i_max_x; x++)
        {
            for (int y = 1; y <= i_max_y; y++)
            {
                if (i_map[x, y] == Floor)
                {
                    if (new Vector3Int(x, y,0) == finishLocation)
                    {
                        maze.SetTile(new Vector3Int(x - (i_max_x / 2), y - (i_max_y / 2), 0), finishTile);
                    }
                    else
                    {
                        maze.SetTile(new Vector3Int(x - (i_max_x / 2), y - (i_max_y / 2), 0), floorTile);
                    }
                }
                else if (i_map[x,y]==Wall)
                {
                    if (finishLocation == new Vector3Int(x, y, 0))
                    {
                        continue;
                    }
                    maze.SetTile(new Vector3Int(x - (i_max_x / 2), y - (i_max_y / 2), 0), wallTile);
                }
            }
        }
    }
}