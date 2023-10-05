using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    private bool isDebuffed = false;
    private bool ikasumi = false;
    public SpriteRenderer ikaRenderer;

    [NonSerialized]
    public PlayerMove move;
    private bool enemySend = false;
    public GameObject Enemy;
    private void Start()
    {
        move = gameObject.GetComponent<PlayerMove>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.CompareTag("haste"))
        {
            if (!isDebuffed)
            {
                isDebuffed = true;
                move.moveSpeed *= 1.3f;
                Invoke("Removebuff", 5.0f);
            }
            else
            {
                if (IsInvoking("Removebuff"))
                {
                    Debug.Log("IsInvoking is true");
                    CancelInvoke("Removebuff");
                    Invoke("Removebuff", 5.0f);
                }
            }
        }
       
        if (collision.gameObject.CompareTag("squid"))
        {
            if (!ikasumi)
            {
                ikasumi = true;
                ikaRenderer.enabled = true;
                Invoke("ikasumiclean", 5.0f);
                Debug.Log("pppp");
            }
            else
            {
                if (IsInvoking("ikasumiclean"))
                {
                    Debug.Log("ow2");
                    CancelInvoke("ikasumiclean");
                    Invoke("ikasumiclean", 5.0f);
                }
            }
        }
        if(collision.gameObject.CompareTag("questionmark"))
        {
            Debug.Log("shadowverse");
            enemySend = true;
            EnemySend();
        }
    }
   
    private void Removebuff()
    {
        move.moveSpeed /= 1.3f;
        isDebuffed = false;
    }
    
    private void ikasumiclean()
    {
        ikaRenderer.enabled = false;
        ikasumi = false;
    }
    private void EnemySend()
    {
        Instantiate(Enemy,transform.position,Quaternion.identity);
    }
}
