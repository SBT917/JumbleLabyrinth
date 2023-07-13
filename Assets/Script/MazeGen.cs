
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeGen : MonoBehaviour
{
    //ƒ^ƒCƒ‹ƒ}ƒbƒv“Ç‚İ‚İ—p
    [SerializeField]
    private Tilemap tilemap1;
    [SerializeField]
    private TileBase tiledata0, tiledata9;

    //–À˜H‚ÌÅ‘å”
    [SerializeField]
    private int i_max_x, i_max_y;

    //ì¬‚µ‚½–À˜H‚ÌŠi”[•Ï”
    int[,] i_map;

    


    // Start is called before the first frame update
    void Start()
    {


        //–_“|‚µ•û‚Å–À˜Hì¬

        int i_temp;

        i_map = new int[i_max_x + 1, i_max_y + 1];

        //ŠO•Ç‚ğ•Ç‚Å–„‚ß‚é
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

        //ƒ}ƒbƒv‚É”½‰f –À˜H‚Ì^‚ñ’†‚ğƒXƒ^[ƒg‚É‚µ‚½‚¢‚Ì‚ÅA”½‰fêŠ‚ğ”¼•ª‚¸‚ç‚µ‚Ä‚¢‚é 0’Ê‰ß 9•Ç
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