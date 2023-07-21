using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour, IKnockBackable
{
    bool isKnockBack;
    Vector3 direction;
    float force;
    float time;

    IInvisiblable invisiblable;

    public bool IsKnockBack { get => isKnockBack; }

    void Awake()
    {
        TryGetComponent(out  invisiblable);
    }

    void FixedUpdate()
    {
        if (isKnockBack)
        {
            UpdateKnockBack();
        }
    }

    public void StartKnockBack(Vector3 direction, float force, float time)
    {
        if (invisiblable.IsInvisible) return;

        isKnockBack = true;
        this.direction = direction;
        this.force = force;
        this.time = time;
    }

    public void UpdateKnockBack()
    {
        transform.position += direction.normalized * force * Time.deltaTime;
        time -= Time.deltaTime;
        if (time <= 0)
        {
            EndKnockBack();
        }
    }

    public void EndKnockBack()
    {
        isKnockBack = false;
    }
}
