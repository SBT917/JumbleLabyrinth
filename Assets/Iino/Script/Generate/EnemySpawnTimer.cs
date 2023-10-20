using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTimer : MonoBehaviour
{
    [SerializeField]
    private float spawnTimer;

    [SerializeField]
    private int minSpawnNum;

    [SerializeField]
    private int maxSpawnNum;

    [SerializeField]
    private GameObject[] spawnEnemies;

    [SerializeField]
    private int enemySpawnMax;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTimer());
    }

    IEnumerator SpawnTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTimer);

            
            if(EnemySpawnManager.instance.EnemyCount < enemySpawnMax )
            {
                for (int i = 0; i <= 1; i++)
                {
                    EnemySpawnManager.instance.RandomSpawnEnemy(Random.Range(minSpawnNum, maxSpawnNum), i, spawnEnemies);
                }
            }
        }
    }
}
