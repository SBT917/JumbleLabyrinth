using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    IMoveable moveable;
    Vector3 direction;

    // Start is called before the first frame update
    void Awake()
    {
        TryGetComponent(out animator);
        TryGetComponent(out moveable);
    }

    // Update is called once per frame
    void Update()
    {
        if (moveable.GetDirection() != Vector3.zero)
        {
            animator.SetBool("isMove", true);
        }
        else
        {
            animator.SetBool("isMove", false);
            return;
        }

        //プレイヤーの向きを変更
        direction = moveable.GetDirection();
        animator.SetFloat("DirectionX", direction.x);
        animator.SetFloat("DirectionY", direction.y);
    }
}
