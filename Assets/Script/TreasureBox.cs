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
        itemImage.gameObject.SetActive(false); // 初めは非表示にする
    }

    public void OpenBox()
    {
        // itemSprites配列の要素数を確認
        if (itemSprites.Length == 0)
        {
            Debug.LogError("itemSprites配列が空です。アイテム画像を追加してください。");
            return;
        }

        // ランダムなインデックスを生成
        int randomIndex = Random.Range(0, itemSprites.Length);

        // アイテムの画像を表示する
        itemImage.sprite = itemSprites[randomIndex];
        itemImage.gameObject.SetActive(true);
    }
}
