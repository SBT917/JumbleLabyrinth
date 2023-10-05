using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvent : MonoBehaviour
{
    const int NEED_COIN_COUNT = 10;
    public SendSignal signal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal"))
        {
            if (TryGetComponent(out ICoinCollecter coinCollecter))
                if (coinCollecter.GetCoinCount() < NEED_COIN_COUNT) return;

            Debug.Log(gameObject.name + "Goal");
            signal.Raise();
        }
    }
}
