using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICoinCollecter
{
    int GetCoinCount();

    void CollectCoin(int count);

    void LoseCoin(int count);
}
