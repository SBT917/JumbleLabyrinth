using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stan : MonoBehaviour, IStanable
{
    bool isStan;
    float count;
    IMoveable moveable;
    IAttackable attackable;

    public bool IsStan { get => isStan; }

    void Awake()
    {
        TryGetComponent(out moveable);
        TryGetComponent(out attackable);
    }

    void Update()
    {
        if (isStan)
        {
            UpdateStan();
        }
    }
    
    public void StartStan(float time)
    {
        isStan = true;
        count = time;
        moveable.Enable = false;
        attackable.Enable = false;
    }

    public void UpdateStan()
    {
        count -= Time.deltaTime;
        if(count <= 0)
        {
            EndStan();
        }
    }

    public void EndStan()
    {
        isStan = false;
        moveable.Enable = true;
        attackable.Enable = true;
    }
}
