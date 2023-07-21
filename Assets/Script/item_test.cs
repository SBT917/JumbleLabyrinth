using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_test : MonoBehaviour
{
    public Sprite[] sprites; // �\������摜�̔z��
    public GameObject treasureBoxPrefab; // �󔠂̃v���n�u
    public float imageSize = 1.0f; // �摜�̃T�C�Y

    private SpriteRenderer spriteRenderer;
    private bool hasBeenClicked = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if (hasBeenClicked)
            return;

        // �N���b�N�̔������~
        hasBeenClicked = true;

        // 2�b��Ƀ����_���ȉ摜��\�����邽�߂̒x������
        Invoke("ShowRandomImage", 1.5f);
    }

    private void ShowRandomImage()
    {
        // �����_���ȉ摜��I��
        Sprite randomSprite = GetRandomSprite();

        if (randomSprite != null)
        {
            // �V����GameObject�𐶐�
            GameObject newObject = Instantiate(treasureBoxPrefab, transform.position, Quaternion.identity);

            // �V����GameObject��SpriteRenderer�R���|�[�l���g���A�^�b�`
            SpriteRenderer newSpriteRenderer = newObject.AddComponent<SpriteRenderer>();
            newSpriteRenderer.sprite = randomSprite;

            // �摜�̃T�C�Y�𒲐�
            newObject.transform.localScale = new Vector3(imageSize, imageSize, 1.0f);

            // �V����GameObject��󔠂̑O�Ɉړ�
            newObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f);
        }
    }

    private Sprite GetRandomSprite()
    {
        if (sprites.Length == 0)
        {
            Debug.LogWarning("�摜��������܂���BSprites�z��ɉ摜��ǉ����Ă��������B");
            return null;
        }

        // �����_���ȃC���f�b�N�X�𐶐�
        int randomIndex = Random.Range(0, sprites.Length);
        return sprites[randomIndex];
    }
    private void HandleItemEffect(string itemName)
    {
        // �A�C�e���̖��O�ɂ���ď�����؂�ւ�
        switch (itemName)
        {
            case "questionmark":
                // questionmark�̌��ʂ��v���C���[�ɓK�p���鏈��
                Debug.Log("questionmark�̌��ʂ𔭓����܂����B");
                break;
            case "slowly_boots":
                // slowly_boots�̌��ʂ��v���C���[�ɓK�p���鏈��
                Debug.Log("slowly_boots�̌��ʂ𔭓����܂����B");
                break;
            case "squid":
                // squid�̌��ʂ��v���C���[�ɓK�p���鏈��
                Debug.Log("squid�̌��ʂ𔭓����܂����B");
                break;
            default:
                Debug.LogWarning("���m�̃A�C�e�����ł��B");
                break;
        }
    }
}
