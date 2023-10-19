using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int count = 1;
    [SerializeField] float destroyCount = 10.0f;

    private void Awake()
    {
        StartCoroutine(DestroyCoroutine());
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(destroyCount);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ICoinCollecter coinCollecter))
        {
            AudioManager.instance.PlaySE("Coin");
            coinCollecter.CollectCoin(count);
            Destroy(gameObject);
        }
    }
}
