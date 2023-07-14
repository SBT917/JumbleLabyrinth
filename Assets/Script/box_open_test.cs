using UnityEngine;
using UnityEngine.EventSystems;

public class box_open_test : MonoBehaviour, IPointerClickHandler
{
    public GameObject[] itemObjects; // �A�C�e���̃Q�[���I�u�W�F�N�g�z��

    public void OnPointerClick(PointerEventData eventData)
    {
        // �����_���ȃA�C�e���̃C���f�b�N�X���擾
        int randomIndex = Random.Range(0, itemObjects.Length);

        // �����_���ɑI�����ꂽ�A�C�e����\������
        for (int i = 0; i < itemObjects.Length; i++)
        {
            if (i == randomIndex)
            {
                itemObjects[i].SetActive(true);
            }
            else
            {
                itemObjects[i].SetActive(false);
            }
        }
    }
}