using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKnockBackable
{
    public bool IsKnockBack { get; }
    public void StartKnockBack(Vector3 direction, float force, float time);
    public void UpdateKnockBack();
    public void EndKnockBack();
    
}
