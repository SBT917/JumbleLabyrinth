using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollecter : MonoBehaviour, ICoinCollecter
{

    private int coinCount; //�R�C���̖���
    private IInvisiblable invisiblable; 

    public Action<int> onCollectCoin; //�R�C�����擾�����Ƃ��ɔ��s�����C�x���g
    public Action<int> onLoseCoin; //�R�C���𗎂Ƃ����Ƃ��ɔ��s�����C�x���g

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
        coinCount -= count;
        if (coinCount < 0) { coinCount = 0; return; }
        onLoseCoin?.Invoke(coinCount);
        Debug.Log(coinCount);
    }
}
