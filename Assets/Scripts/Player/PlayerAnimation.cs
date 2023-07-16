using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    Vector3 direction;

    IMoveable moveable;
    IStanable stanable;

    // Start is called before the first frame update
    void Awake()
    {
        TryGetComponent(out animator);
        TryGetComponent(out moveable);
        TryGetComponent(out stanable);
    }

    // Update is called once per frame
    void Update()
    {
        MoveAnimation();
        StanAnimation();
    }

    void MoveAnimation()
    {
        if (!moveable.Enable) return;

        if (moveable.Direction != Vector3.zero)
        {
            animator.SetBool("isMove", true);
        }
        else
        {
            animator.SetBool("isMove", false);
            return;
        }

        //プレイヤーの向きを変更
        direction = moveable.Direction;
        animator.SetFloat("DirectionX", direction.x);
        animator.SetFloat("DirectionY", direction.y);
    }

    void StanAnimation()
    {
        animator.SetBool("isStan", stanable.IsStan);
    }
}