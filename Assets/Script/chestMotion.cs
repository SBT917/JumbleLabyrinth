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
        animator.enabled = false; // Animatorを無効にしておく
    }

    private void OnMouseDown()
    {
        if (!hasBeenClicked)
        {
            animator.enabled = true; // マウスクリック時にAnimatorを有効にする
            hasBeenClicked = true;
        }
    }
}
