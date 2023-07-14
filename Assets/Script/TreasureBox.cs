using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureBox : MonoBehaviour
{
    public Image itemImage;
    public Sprite[] itemSprites;

    private void Start()
    {
        itemImage.gameObject.SetActive(false); // ���߂͔�\���ɂ���
    }

    public void OpenBox()
    {
        // itemSprites�z��̗v�f�����m�F
        if (itemSprites.Length == 0)
        {
            Debug.LogError("itemSprites�z�񂪋�ł��B�A�C�e���摜��ǉ����Ă��������B");
            return;
        }

        // �����_���ȃC���f�b�N�X�𐶐�
        int randomIndex = Random.Range(0, itemSprites.Length);

        // �A�C�e���̉摜��\������
        itemImage.sprite = itemSprites[randomIndex];
        itemImage.gameObject.SetActive(true);
    }
}
