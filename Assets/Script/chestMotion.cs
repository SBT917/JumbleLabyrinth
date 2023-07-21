using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestMotion : MonoBehaviour
{
    private Animator animator;
    private bool hasBeenClicked = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false; // Animator�𖳌��ɂ��Ă���
    }

    private void OnMouseDown()
    {
        if (!hasBeenClicked)
        {
            animator.enabled = true; // �}�E�X�N���b�N����Animator��L���ɂ���
            hasBeenClicked = true;
        }
    }
}
