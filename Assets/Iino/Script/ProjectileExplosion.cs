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
            //TODO:�v���C���[���X�^��������
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
