using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class box : MonoBehaviour, IPointerClickHandler
{
    public Sprite[] itemSprites; // �A�C�e���̃X�v���C�g�z��
    public Image itemImage; // �A�C�e����\������C���[�W�R���|�[�l���g

    public void OnPointerClick(PointerEventData eventData)
    {
        // �����_���ȃA�C�e���̃C���f�b�N�X���擾
        int randomIndex = Random.Range(0, itemSprites.Length);

        // �A�C�e���̃X�v���C�g��ݒ肷��
        itemImage.sprite = itemSprites[randomIndex];
    }
}