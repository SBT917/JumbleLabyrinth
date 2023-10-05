using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//シングルトンのジェネリッククラス
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance;

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

