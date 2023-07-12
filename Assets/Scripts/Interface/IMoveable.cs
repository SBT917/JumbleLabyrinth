using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }


    /**  
    <summary>
    移動処理
    </summary>
    */
    void Move(Vector3 direction);

    /**  
    <summary>
    移動する方向を設定する
    </summary>
    */
    void SetDirection(Vector3 direction);

    /**  
    <summary>
    移動している方向を取得する
    </summary>
    */
    Vector3 GetDirection();

    /**  
    <summary>
    移動速度を設定する
    </summary>
    */
    void SetSpeed(float speed);

    /**  
    <summary>
    移動速度を取得する
    </summary>
    */
    float GetSpeed();
}
