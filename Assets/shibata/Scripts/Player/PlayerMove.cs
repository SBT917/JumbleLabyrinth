using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour, IMoveable
{
    [SerializeField] PlayerStatus status;

    bool enable = true;
    Rigidbody2D rigid;
    float moveSpeed; //移動速度
    Vector3 moveVec; //移動ベクトル

    public bool Enable
    {
        get => enable;
        set => enable = value; 
    }

    public float Speed 
    { 
        get => moveSpeed;
        set => moveSpeed = value; 
    } 

    public Vector3 Direction 
    { 
        get => moveVec;
        set => moveVec = value; 
    } 

    void Awake()
    {
        TryGetComponent(out rigid);
        moveSpeed = status.defaultMoveSpeed;
    }

    void FixedUpdate()
    { 
        Move(moveVec);
    }

    public void Move(Vector3 direction)
    {
        if (!enable) return;
        Vector3 move = transform.position + direction * moveSpeed * Time.deltaTime;
        rigid.MovePosition(move);
    }
}

