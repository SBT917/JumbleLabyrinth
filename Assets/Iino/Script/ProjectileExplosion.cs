using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExplosion : MonoBehaviour
{
    [SerializeField]
    private string AudioName;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlaySE(AudioName);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.TryGetComponent(out IInvisiblable invisiblable);
            if (invisiblable.IsInvisible)
            {
                return;
            }

            if (collision.transform.TryGetComponent(out IKnockBackable knockBackable))
            {
                Vector2 dir = collision.transform.position - transform.position;
                knockBackable.StartKnockBack(dir, 2f, 0.1f);
            }

            if (collision.transform.TryGetComponent(out IStanable stanable))
            {
                stanable.StartStan(1f);
            }

            if (collision.transform.TryGetComponent(out ICoinCollecter coinCollecter))
            {
                coinCollecter.LoseCoin(1);
            }

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
