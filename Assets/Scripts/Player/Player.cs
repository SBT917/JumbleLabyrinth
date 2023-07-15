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


    //スタン時処理
    bool isStan = false;
    void Awake()
    {
        TryGetComponent(out input);
        TryGetComponent(out moveable);
    }

    public void StartStan(float time)
    {
        isStan = true;
        StartCoroutine(StanCoroutine(time));
    }

    public bool IsStan()
    {
        return isStan;
    }

    IEnumerator StanCoroutine(float time)
    {
        input.enabled = false;
        yield return new WaitForSeconds(time);
        input.enabled = true;
        isStan = false;
    }


    //ノックバック時処理
    bool isKnockBack = false;
    public void KnockBack(Vector3 direction, float force)
    {
        isKnockBack = true;
        StartCoroutine(KnockBackCoroutine(direction, force));
    }

    public bool IsKnockBack()
    {
        return isKnockBack;
    }

    IEnumerator KnockBackCoroutine(Vector3 direction , float force)
    {
        float defaultSpeed = moveable.GetSpeed();

        moveable.SetDirection(direction);
        moveable.SetSpeed(force);

        yield return new WaitForSeconds(0.1f);

        moveable.SetDirection(Vector3.zero);
        moveable.SetSpeed(defaultSpeed);
        isKnockBack = false;

    }

}
