using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    public delegate void TriggerEventDelegate(Collider2D collision);
    public TriggerEventDelegate onTriggerEnter;
    public TriggerEventDelegate onTriggerExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(onTriggerEnter != null)
        {
            onTriggerEnter(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(onTriggerExit != null)
        {
            onTriggerExit(collision);
        }

    }
}
