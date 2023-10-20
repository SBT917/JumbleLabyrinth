using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public bool isFadeout = false;

    [SerializeField]
    Image fade;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Color_FadeIn");
    }
    IEnumerator Color_FadeIn()
    {
       // Image fade=GetComponent<Image>();
        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (255.0f / 255.0f));

        // フェードインにかかる時間
        const float fade_time = 2.0f;

        // ループ回数
        const int loop_count = 10;

        // ウェイト時間算出
        float wait_time = fade_time / loop_count;

        // 色の間隔を算出 
        float alpha_interval = 255.0f / loop_count;

        // 色を徐々に変えるループ
        for (float alpha = 255.0f; alpha >= 0.0f; alpha -= alpha_interval)
        {
            // 待ち時間
            yield return new WaitForSeconds(wait_time);

            // Alpha値を少しずつ下げる
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
        }
    }
    IEnumerator Color_FadeOut()
    {
        //Image fade=GetComponent<Image>();   
        // フェード元の色を設定
        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (0.0f / 255.0f));

        // フェードアウトにかかる時間
        const float fade_time = 2.0f;

        // ループ回数
        const int loop_count = 10;

        // ウェイト時間算出
        float wait_time = fade_time / loop_count;

        // 色の間隔を算出
        float alpha_interval = 1.0f / loop_count;

        // 色を徐々に変えるループ
        for (float alpha = 0.0f; alpha <= 2.0f; alpha += alpha_interval)
        {
            // 待ち時間
            yield return new WaitForSeconds(wait_time);

            // Alpha値を少しずつ上げる
            Color new_color = fade.color;
            new_color.a = alpha;
            fade.color = new_color;
        }
    }
    private void Update()
    {
        if(isFadeout)
        {
            isFadeout = false;
            StartCoroutine("Color_FadeOut");
        }
    }
}

