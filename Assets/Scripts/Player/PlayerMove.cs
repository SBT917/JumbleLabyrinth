using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour, IMoveable
{
    [SerializeField] PlayerStatus status;

    Rigidbody2D rigid;
    float moveSpeed; //移動速度
    Vector3 moveVec; //移動ベクトル    

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
        Vector3 move = transform.position + direction * moveSpeed * Time.deltaTime;
        rigid.MovePosition(move);
    }

    public void SetDirection(Vector3 direction)
    {
        moveVec = direction;
    }

    public Vector3 GetDirection()
    {
        return moveVec;
    }
    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public float GetSpeed()
    {
        return moveSpeed;
    }
}

