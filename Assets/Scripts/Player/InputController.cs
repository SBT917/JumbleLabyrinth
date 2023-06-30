using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    PlayerInput input; //InputSystem;
    IMoveable move;
    IAttackable attack;

    void Awake()
    {
        TryGetComponent(out input);
        TryGetComponent(out move);
        TryGetComponent(out attack);
    }

    void OnEnable()
    {
        input.actions["Move"].performed += OnMove;
        input.actions["Move"].canceled += OnMoveStop;
        input.actions["Attack"].started += OnAttack;
    }

    void OnDisable()
    {
        input.actions["Move"].performed -= OnMove;
        input.actions["Move"].canceled -= OnMoveStop;
        input.actions["Attack"].started -= OnAttack;
    }

    //移動キーを押したときの処理
    void OnMove(InputAction.CallbackContext context)
    {
        Vector3 value = context.ReadValue<Vector2>();
        move.SetDirection(value);
    }

    //移動キーを離したときの処理
    void OnMoveStop(InputAction.CallbackContext context)
    {
        move.SetDirection(Vector3.zero);
    }

    //攻撃キーを押したときの処理
    void OnAttack(InputAction.CallbackContext context)
    {
        attack.Attack();
    }

}
