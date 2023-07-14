using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class box : MonoBehaviour, IPointerClickHandler
{
    public Sprite[] itemSprites; // アイテムのスプライト配列
    public Image itemImage; // アイテムを表示するイメージコンポーネント

    public void OnPointerClick(PointerEventData eventData)
    {
        // ランダムなアイテムのインデックスを取得
        int randomIndex = Random.Range(0, itemSprites.Length);

        // アイテムのスプライトを設定する
        itemImage.sprite = itemSprites[randomIndex];
    }
}