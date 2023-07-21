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
            if (collision.transform.TryGetComponent(out IKnockBackable knockBackable))
            {
                Vector2 dir = collision.transform.position - transform.position;
                knockBackable.StartKnockBack(dir, 2f, 0.1f);
            }

            if (collision.transform.TryGetComponent(out IStanable stanable))
            {
                stanable.StartStan(1f);
            }

            //�R���C�_�[���I�t��
            GetComponent<Collider2D>().enabled = false;
        }
    }

    //�A�j���[�V�����̍Đ���ɌĂяo�����
    public void DestroyExplosion()
    {
        Destroy(gameObject);
    }
}
