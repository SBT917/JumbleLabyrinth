using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerater : MonoBehaviour
{
    // �V���O���g��
    public static EnemyGenerater instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    [SerializeField]
    private GameObject[] enemies;
    
    private int playerNum = 2;
    // Start is called before the first frame update
    void Start()
    {
        GenerateFirstEnemy();
    }


    private void GenerateFirstEnemy()
    {
        for (int i = 0; i < playerNum; ++i)
        {
            for (int j = 0; j < enemies.Length; j++)
            {
                for (int k = 0; k < enemies[j].GetComponent<Enemy>().firstGenetrateNum; k++)
                {
                    var spawnPosition = WalkableTilesManager.instance.GetRandomPoint(i);
                    if (!spawnPosition.HasValue) // null�`�F�b�N
                    {
                        Debug.LogError($"No free tiles available for player {i}.");
                        break;  // �󂫃^�C����������Ȃ������ꍇ�A�v���C���[��ς���
                    }

                    var enemy = Instantiate(enemies[j], spawnPosition.Value, Quaternion.identity, transform);
                    enemy.GetComponent<Enemy>().playerID = i;
                }
            }
        }
    }


    void Update()
    {
        
    }
}
