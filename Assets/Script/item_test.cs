using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_test : MonoBehaviour
{
    public Sprite[] sprites; // 表示する画像の配列
    public GameObject treasureBoxPrefab; // 宝箱のプレハブ
    public float imageSize = 1.0f; // 画像のサイズ

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

        // クリックの反応を停止
        hasBeenClicked = true;

        // 2秒後にランダムな画像を表示するための遅延処理
        Invoke("ShowRandomImage", 1.5f);
    }

    private void ShowRandomImage()
    {
        // ランダムな画像を選択
        Sprite randomSprite = GetRandomSprite();

        if (randomSprite != null)
        {
            // 新しいGameObjectを生成
            GameObject newObject = Instantiate(treasureBoxPrefab, transform.position, Quaternion.identity);

            // 新しいGameObjectにSpriteRendererコンポーネントをアタッチ
            SpriteRenderer newSpriteRenderer = newObject.AddComponent<SpriteRenderer>();
            newSpriteRenderer.sprite = randomSprite;

            // 画像のサイズを調整
            newObject.transform.localScale = new Vector3(imageSize, imageSize, 1.0f);

            // 新しいGameObjectを宝箱の前に移動
            newObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f);
        }
    }

    private Sprite GetRandomSprite()
    {
        if (sprites.Length == 0)
        {
            Debug.LogWarning("画像が見つかりません。Sprites配列に画像を追加してください。");
            return null;
        }

        // ランダムなインデックスを生成
        int randomIndex = Random.Range(0, sprites.Length);
        return sprites[randomIndex];
    }
    private void HandleItemEffect(string itemName)
    {
        // アイテムの名前によって処理を切り替え
        switch (itemName)
        {
            case "questionmark":
                // questionmarkの効果をプレイヤーに適用する処理
                Debug.Log("questionmarkの効果を発動しました。");
                break;
            case "slowly_boots":
                // slowly_bootsの効果をプレイヤーに適用する処理
                Debug.Log("slowly_bootsの効果を発動しました。");
                break;
            case "squid":
                // squidの効果をプレイヤーに適用する処理
                Debug.Log("squidの効果を発動しました。");
                break;
            default:
                Debug.LogWarning("未知のアイテム名です。");
                break;
        }
    }
}
