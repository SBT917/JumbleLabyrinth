using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    // シングルトン
    public static EnemySpawnManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    [SerializeField]
    private GameObject[] enemies;

    [SerializeField]
    private GameObject[] players;

    [SerializeField]
    private GameObject respawnEffect;

    public GameObject[] sendTrails;
    
    private int playerNum;
    // Start is called before the first frame update
    void Start()
    {
        playerNum = players.Length;
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
                    if (!spawnPosition.HasValue) // nullチェック
                    {
                        Debug.LogError($"No free tiles available for player {i}.");
                        break;  // 空きタイルが見つからなかった場合、プレイヤーを変える
                    }

                    var enemy = Instantiate(enemies[j], spawnPosition.Value, Quaternion.identity, transform);
                    enemy.GetComponent<Enemy>().playerID = i;
                }
            }
        }
    }

    public void RespawnEnemy(int enemyID, int nextSpawnMapID, Vector3 currentEnemyPosition,GameObject SendTrail)
    {
        var spawnPosition = WalkableTilesManager.instance.GetRandomPointNearPlayer(nextSpawnMapID, players[nextSpawnMapID].transform.position, 5, 2);
        var effect = Instantiate(SendTrail, currentEnemyPosition, Quaternion.identity);
        effect.GetComponent<SendEffectMove>().StartMoving(spawnPosition.Value);

        StartCoroutine(DelaySpawnEnemy(enemyID, nextSpawnMapID, spawnPosition.Value));
    }


    public IEnumerator DelaySpawnEnemy(int enemyID, int nextSpawnMapID, Vector3 spawnPosition)
    {
        // 3秒待機
        yield return new WaitForSeconds(1f);
        var respawnEnemy = Instantiate(enemies[enemyID], spawnPosition, Quaternion.identity);
        respawnEnemy.GetComponent<Enemy>().playerID = nextSpawnMapID;
    }


}
