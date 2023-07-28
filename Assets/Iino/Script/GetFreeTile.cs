using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GetFreeTile : MonoBehaviour
{
    //�C���X�^���X��
    public static GetFreeTile instance;
    private void Awake()
    {
        if (instance == null)
        instance = this;
        else
        Destroy(this);
    }

    //�^�C���}�b�v�ǂݍ��ݗp
    [SerializeField]
    private Tilemap tilemap;

    //�󂫃^�C���̊i�[�ϐ�
    private List<Vector3Int> freeTiles;
    // Start is called before the first frame update
    void Start()
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        freeTiles = new List<Vector3Int>();

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                for (int z = bounds.zMin; z < bounds.zMax; z++)
                {
                    Vector3Int localPlace = (new Vector3Int(x, y, z));
                    Vector3Int tileLocation = new Vector3Int(localPlace.x, localPlace.y, localPlace.z);

                    if (tilemap.GetTile(tileLocation) == null)
                    {
                        freeTiles.Add(tileLocation);
                    }
                }
            }
        }

    }

    //�󂫃^�C����Ԃ�
    public List<Vector3Int> GetFreeTiles()
    {
        return freeTiles;
    }
    
}
