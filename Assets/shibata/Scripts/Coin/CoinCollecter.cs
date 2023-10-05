using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollecter : MonoBehaviour, ICoinCollecter
{

    private int coinCount; //�R�C���̖���
    public Action<int> onCollectCoin; //�R�C�����擾�����Ƃ��ɔ��s�����C�x���g
    public Action<int> onLoseCoin; //�R�C���𗎂Ƃ����Ƃ��ɔ��s�����C�x���g

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
