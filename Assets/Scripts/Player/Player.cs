using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput),typeof(IMoveable))]
public class Player : MonoBehaviour, IStanable, IKnockBackable
{
    PlayerInput input;
    IMoveable moveable;

    void Awake()
    {
        TryGetComponent(out input);
        TryGetComponent(out moveable);
    }


    //スタン時処理
    bool isStan = false;
    public bool IsStan { get => isStan; }
    public void StartStan(float time)
    {
        isStan = true;
        StartCoroutine(StanCoroutine(time));
    }

    IEnumerator StanCoroutine(float time)
    {
        input.enabled = false;
        yield return new WaitForSeconds(time);
        input.enabled = true;
        isStan = false;
    }


    //ノックバック時処理
    bool isKnockBack;
    public bool IsKnockBack { get => isKnockBack; }
    public void KnockBack(Vector3 direction, float force)
    {
        isKnockBack = true;
        StartCoroutine(KnockBackCoroutine(direction, force));
    }

    IEnumerator KnockBackCoroutine(Vector3 direction , float force)
    {
        float defaultSpeed = moveable.Speed;

        moveable.Direction = direction;
        moveable.Speed = force;

        yield return new WaitForSeconds(0.1f);

        moveable.Direction = Vector3.zero;
        moveable.Speed = defaultSpeed;
        isKnockBack = false;

    }

}
