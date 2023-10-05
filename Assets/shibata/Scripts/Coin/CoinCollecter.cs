using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollecter : MonoBehaviour, ICoinCollecter
{

    private int coinCount; //コインの枚数
    public Action<int> onCollectCoin; //コインを取得したときに発行されるイベント
    public Action<int> onLoseCoin; //コインを落としたときに発行されるイベント

    public int GetCoinCount()
    {
        return coinCount;
    }

    public void CollectCoin(int count)
    {
        coinCount += count;
        onCollectCoin?.Invoke(coinCount);
    }

    public void LoseCoin(int count)
    {
        coinCount -= count;
        if(coinCount < 0) { coinCount = 0; return; }
        onLoseCoin?.Invoke(coinCount);
    }
}
