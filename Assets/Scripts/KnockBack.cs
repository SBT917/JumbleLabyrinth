using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour, IKnockBackable
{
    bool isKnockBack;
    Vector3 direction;
    float force;
    float time;

    public bool IsKnockBack { get => isKnockBack; }

    void FixedUpdate()
    {
        if (isKnockBack)
        {
            UpdateKnockBack();
        }
    }

    public void StartKnockBack(Vector3 direction, float force, float time)
    {
        isKnockBack = true;
        this.direction = direction;
        this.force = force;
        this.time = time;
    }

    public void UpdateKnockBack()
    {
        transform.position += direction * force * Time.deltaTime;
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
