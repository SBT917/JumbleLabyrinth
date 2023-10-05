using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFlame : MonoBehaviour
{
    [SerializeField]
    private GameObject flame;

    public float GenerateTime;
    public float GenerateTimeCnt;

    public int GenerateNum;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GenerateTimeCnt += Time.deltaTime;
        if(GenerateTimeCnt >= GenerateTime)
        {
            GenerateTimeCnt = 0;
            for(int i = 0; i < GenerateNum;++i)
            {
                GenerateFlame(Random.Range(0, 1));
            }
            
        }
    }

    private void GenerateFlame(int playerNum)
    {
        var spawnPosition = WalkableTilesManager.instance.GetRandomPoint(playerNum);
        Instantiate(flame, spawnPosition.Value, Quaternion.identity, transform);
    }
}
