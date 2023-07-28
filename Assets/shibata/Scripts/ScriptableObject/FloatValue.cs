using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    public float initValue;
    
    
    public float runtimeValue;
    public void OnAfterDeserialize()
    {
        runtimeValue = initValue;
    }
    public void OnBeforeSerialize()
    {
        
    }
}
