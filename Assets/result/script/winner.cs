using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winner : MonoBehaviour
{
    public Animator animator1;
    public Animator animator2;

    public void Start()
    {
        // �ŏ��͗����̃A�j���[�^�̎p����false�ɂ���
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
            Debug.LogError("Animator�R���|�[�l���g���A�^�b�`����Ă��܂���B");
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
            Debug.LogError("�����ȏ��Ҕԍ��ł��B1��2���w�肵�Ă��������B");
        }
    }
    
}
