using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int count = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ICoinCollecter coinCollecter))
        {
            coinCollecter.CollectCoin(count);
            Destroy(gameObject);
        }
    }
}
