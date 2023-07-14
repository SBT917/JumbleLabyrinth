using UnityEngine;
using UnityEngine.EventSystems;

public class box_open_test : MonoBehaviour, IPointerClickHandler
{
    public GameObject[] itemObjects; // アイテムのゲームオブジェクト配列

    public void OnPointerClick(PointerEventData eventData)
    {
        // ランダムなアイテムのインデックスを取得
        int randomIndex = Random.Range(0, itemObjects.Length);

        // ランダムに選択されたアイテムを表示する
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