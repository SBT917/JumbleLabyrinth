using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    [SerializeField]
    TextMeshProUGUI enemyCountText;

    public GameObject[] sendTrails;
    
    private int playerNum;

    public int EnemyCount { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        playerNum = players.Length;
        GenerateFirstEnemy();
    }

    private void Update()
    {
        //enemyCountText.text = EnemyCount.ToString();
    }


    public void GenerateFirstEnemy()
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
                    AddEnemyCount(1);
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

        StartCoroutine(DelaySpawnEnemy(enemyID, nextSpawnMapID, spawnPosition.Value,1f));
    }


    public IEnumerator DelaySpawnEnemy(int enemyID, int nextSpawnMapID, Vector3 spawnPosition,float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        AddEnemyCount(1);
        var respawnEnemy = Instantiate(enemies[enemyID], spawnPosition, Quaternion.identity);
        respawnEnemy.GetComponent<Enemy>().playerID = nextSpawnMapID;
    }

    //指定された数ランダムに敵を生成
    public void RandomSpawnEnemy(int spawnNum,int spawnMapID, GameObject[] enemies)
    {
        for(int i = 0; i < spawnNum; i++)
        {
            var spawnPosition = WalkableTilesManager.instance.GetRandomPoint(spawnMapID);
            AddEnemyCount(spawnNum);
            var respawnEnemy = Instantiate(enemies[Random.Range(0,enemies.Length)], spawnPosition.Value, Quaternion.identity);
            respawnEnemy.GetComponent<Enemy>().playerID = spawnMapID;
        }

    }


    public void AddEnemyCount(int amount)
    {
        EnemyCount += amount;
    }

    public void RemoveEnemyCount(int amount)
    {
        EnemyCount -= amount;
    }
}
