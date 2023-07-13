
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeGen : MonoBehaviour
{
    //タイルマップ読み込み用
    [SerializeField]
    private Tilemap tilemap1;
    [SerializeField]
    private TileBase tiledata0, tiledata9;

    //迷路の最大数
    [SerializeField]
    private int i_max_x, i_max_y;

    //作成した迷路の格納変数
    int[,] i_map;

    


    // Start is called before the first frame update
    void Start()
    {


        //棒倒し方で迷路作成

        int i_temp;

        i_map = new int[i_max_x + 1, i_max_y + 1];

        //外壁を壁で埋める
        for (int i = 1; i <= i_max_x; i++)
        {
            i_map[i, 1] = 9;
            i_map[i, i_max_y] = 9;
        }

        for (int i = 1; i <= i_max_y; i++)
        {
            i_map[1, i] = 9;
            i_map[i_max_x, i] = 9;
        }

        //棒倒し方で迷路を作成(少し手抜き)
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
                switch (Random.Range(0, i_temp))
                {
                    case 0:
                        i_map[x + 1, y] = 9;
                        break;
                    case 1:
                        i_map[x - 1, y] = 9;
                        break;
                    case 2:
                        i_map[x, y + 1] = 9;
                        break;
                    case 3:
                        if (y == 3)
                        {
                            i_map[x, y - 1] = 9;
                        }
                        break;
                }
            }

        }

        //マップに反映 迷路の真ん中をスタートにしたいので、反映場所を半分ずらしている 0通過 9壁
        for (int x = 1; x <= i_max_x; x++)
        {
            for (int y = 1; y <= i_max_y; y++)
            {
                if (i_map[x, y] == 0)
                {
                    tilemap1.SetTile(new Vector3Int(x - (i_max_x / 2), y - (i_max_y / 2), 0), tiledata0);
                }
                else
                {
                    tilemap1.SetTile(new Vector3Int(x - (i_max_x / 2), y - (i_max_y / 2), 0), tiledata9);
                }
            }
        }
    }

}