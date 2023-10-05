using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WalkableTilesManager : MonoBehaviour
{
    // �V���O���g��
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

    // �^�C���}�b�v
    [SerializeField]
    private Tilemap[] tilemaps;

    // �󂫃^�C���̃��X�g
    private List<Vector3Int>[] freeTiles;

    private void Start()
    {

    }

    // �󂫃^�C���̃��X�g���X�V
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
                Vector3Int tileLocation = new Vector3Int(x, y, 0); // 2D�^�C���}�b�v�Ȃ̂�z=0

                if (tilemap.GetTile(tileLocation) == null)
                {
                    freeTiles[playerIndex].Add(tileLocation);
                }
            }
        }
    }

    // ���s�\�^�C���̃��X�g��Ԃ�
    public List<Vector3Int> GetFreeTiles(int playerIndex)
    {
        return freeTiles[playerIndex];
    }

    /// <summary>
    /// �����_���ȕ��s�\�^�C����Ԃ�
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

        // Cell �̒��S�ɃI�t�Z�b�g��ǉ�
        Vector3 centerOffset = new Vector3(0f, 0f, 0);
        Vector3 worldPosition = tilemaps[playerIndex].CellToWorld(localPosition) + centerOffset;

        return worldPosition;
    }


    /// <summary>
    /// �v���C���[�̋߂��ɂ��郉���_���ȕ��s�\�^�C����Ԃ�
    /// </summary>
    /// <param name="playerIndex"></param>
    /// <param name="playerPosition"></param>
    /// <param name="maxDistance"></param>
    /// <returns></returns>
    public Vector3? GetRandomPointNearPlayer(int playerIndex, Vector3 playerPosition, float maxDistance,float minDistance)
    {
        // �v���C���[�̋߂��̕��s�\�ȃ^�C�����i�[���郊�X�g
        List<Vector3Int> tilesNearPlayer = new List<Vector3Int>();

        foreach (Vector3Int tile in freeTiles[playerIndex])
        {
            // �^�C���̃��[���h���W���擾
            Vector3 worldPosition = tilemaps[playerIndex].CellToWorld(tile);

            // �v���C���[�ƃ^�C���̋������v�Z
            float distanceToPlayer = Vector3.Distance(playerPosition, worldPosition);

            // �^�C�����v���C���[�̋߂��ɂ��邩����
            if (distanceToPlayer <= maxDistance && distanceToPlayer >= minDistance)
            {
                tilesNearPlayer.Add(tile);
            }
        }

        // �߂��̃^�C�������݂��Ȃ��ꍇ
        if (tilesNearPlayer.Count == 0)
        {
            Debug.LogError("�w�肳�ꂽ���W�̋߂��ɋ󂫃^�C�������݂��Ȃ�");
            return GetRandomPoint(playerIndex);
        }

        // �߂��̃^�C���̒����烉���_���ɑI��
        int randomIndex = UnityEngine.Random.Range(0, tilesNearPlayer.Count);
        Vector3Int localPosition = tilesNearPlayer[randomIndex];

        // Cell �̒��S�ɃI�t�Z�b�g��ǉ�
        Vector3 centerOffset = new Vector3(0.5f, 0.5f, 0);
        Vector3 worldPositionNearPlayer = tilemaps[playerIndex].CellToWorld(localPosition) + centerOffset;

        Debug.Log("���X�|�[���ꏊ�F" + worldPositionNearPlayer.ToString());
        tilesNearPlayer.Clear();
        return worldPositionNearPlayer;
    }
}

