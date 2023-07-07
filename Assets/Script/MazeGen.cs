
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeGen : MonoBehaviour
{
    //ƒ^ƒCƒ‹ƒ}ƒbƒv“Ç‚İ‚İ—p
    [SerializeField]
    private Tilemap maze;
    [SerializeField]
    private TileBase floorTile, wallTile, finishTile;

    
    //–À˜H‚ÌÅ‘å”
    [SerializeField]
    private int i_max_x, i_max_y;

    //ì¬‚µ‚½–À˜H‚ÌŠi”[•Ï”
    private int[,] i_map;

    private const int Floor = 0, Wall = 9, Finish = -1;


    private Vector2 finishLocation;
    void Start()
    {
        finishLocation = new Vector2Int(Random.Range(3, i_max_x), Random.Range(3, i_max_y));
        Debug.Log(finishLocation);
        GenerateMaze();
    }
    private void GenerateMaze()
    {
        //–_“|‚µ•û‚Å–À˜Hì¬

        int i_temp;

        i_map = new int[i_max_x + 1, i_max_y + 1];

        //ŠO•Ç‚ğ•Ç‚Å–„‚ß‚é
        FillOuterWalls();

        //–_“|‚µ•û‚Å–À˜H‚ğì¬(­‚µè”²‚«)
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

        //ƒ}ƒbƒv‚É”½‰f –À˜H‚Ì^‚ñ’†‚ğƒXƒ^[ƒg‚É‚µ‚½‚¢‚Ì‚ÅA”½‰fêŠ‚ğ”¼•ª‚¸‚ç‚µ‚Ä‚¢‚é 0’Ê‰ß 9•Ç
        for (int x = 1; x <= i_max_x; x++)
        {
            for (int y = 1; y <= i_max_y; y++)
            {
                if (i_map[x, y] == Floor)
                {
                    if (new Vector2Int(x, y) == finishLocation)
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
                    maze.SetTile(new Vector3Int(x - (i_max_x / 2), y - (i_max_y / 2), 0), wallTile);
                }
            }
        }
    }
}