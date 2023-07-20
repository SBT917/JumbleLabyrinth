using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStanable
{
    public bool IsStan { get; }
    public void StartStan(float time);
    public void UpdateStan();
    public void EndStan();
}
