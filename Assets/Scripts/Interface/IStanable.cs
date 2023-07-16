using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStanable
{
    public void StartStan(float time);
    public bool IsStan { get; }
}
