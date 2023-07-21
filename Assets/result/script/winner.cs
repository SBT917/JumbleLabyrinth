using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winner : MonoBehaviour
{
    public Animator animator1;
    public Animator animator2;

    public void Start()
    {
        // 最初は両方のアニメータの姿勢をfalseにする
        if (animator1 != null)
        {
            animator1.SetBool("IsWinner", false);
        }
        if (animator2 != null)
        {
            animator2.SetBool("IsWinner", false);
        }

    }

    public void PlayAnimator(int winner)
    {
        if (animator1 == null || animator2 == null)
        {
            Debug.LogError("Animatorコンポーネントがアタッチされていません。");
            return;
        }

        if (winner == 1)
        {
            animator1.SetBool("IsWinner", true);
            animator2.SetBool("IsWinner", false);
        }
        else if (winner == 2)
        {
            animator1.SetBool("IsWinner", false);
            animator2.SetBool("IsWinner", true);
        }
        else
        {
            Debug.LogError("無効な勝者番号です。1か2を指定してください。");
        }
    }
    
}
