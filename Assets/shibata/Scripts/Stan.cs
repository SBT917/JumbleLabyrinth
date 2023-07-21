using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stan : MonoBehaviour, IStanable
{
    bool isStan;
    float count;
    IMoveable moveable;
    IAttackable attackable;
    IInvisiblable invisiblable;

    public bool IsStan { get => isStan; }

    void Awake()
    {
        TryGetComponent(out moveable);
        TryGetComponent(out attackable);
        TryGetComponent(out invisiblable);
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
        if (invisiblable.IsInvisible) return;

        isStan = true;
        count = time;
        if (moveable != null) moveable.Enable = false;
        if (attackable != null) attackable.Enable = false;

        invisiblable.Invisible();
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
        if(moveable != null) moveable.Enable = true; 
        if(attackable != null) attackable.Enable = true;
    }
}
