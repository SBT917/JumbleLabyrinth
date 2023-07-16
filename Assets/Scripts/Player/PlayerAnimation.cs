using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    Vector3 direction;

    IMoveable moveable;
    IKnockBackable knockBackable;

    // Start is called before the first frame update
    void Awake()
    {
        TryGetComponent(out animator);
        TryGetComponent(out moveable);
        TryGetComponent(out knockBackable);
    }

    // Update is called once per frame
    void Update()
    {
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
        if (knockBackable.IsKnockBack) return;
        direction = moveable.Direction;
        animator.SetFloat("DirectionX", direction.x);
        animator.SetFloat("DirectionY", direction.y);
    }
}