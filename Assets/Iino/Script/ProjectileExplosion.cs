using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //TODO:プレイヤーをスタンさせる
            //コライダーをオフに
            GetComponent<Collider2D>().enabled = false;
        }
    }

    //アニメーションの再生後に呼び出される
    public void DestroyExplosion()
    {
        Destroy(gameObject);
    }
}
