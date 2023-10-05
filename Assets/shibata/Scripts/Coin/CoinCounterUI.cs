using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounterUI : MonoBehaviour
{
    [SerializeField] private CoinCollecter coinCollecter;
    [SerializeField] private TextMeshProUGUI text;

    private int count;
    

    // Start is called before the first frame update
    void Awake()
    {
        coinCollecter.onCollectCoin += UpdateCount;
        UpdateCount(0);
    }

    void UpdateCount(int value)
    {
        count = value;
        text.text = "x" + value.ToString();
    }
}
