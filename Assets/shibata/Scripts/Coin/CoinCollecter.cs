using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollecter : MonoBehaviour, ICoinCollecter
{

    private int coinCount; //コインの枚数
    private IInvisiblable invisiblable; 

    public Action<int> onCollectCoin; //コインを取得したときに発行されるイベント
    public Action<int> onLoseCoin; //コインを落としたときに発行されるイベント

    private void Awake()
    {
        TryGetComponent(out invisiblable);
    }

    public int GetCoinCount()
    {
        return coinCount;
    }

    public void CollectCoin(int count)
    {
        coinCount += count;
        onCollectCoin?.Invoke(coinCount);
        Debug.Log(coinCount);
    }

    public void LoseCoin(int count)
    {
        if (coinCount <= 0) return;

        coinCount -= count;
        onLoseCoin?.Invoke(coinCount);
        Debug.Log(coinCount);
    }
}
