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

    [SerializeField]
    private int SendEnemyID;

    [SerializeField]
    private int SendPlayerID;
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
                AudioManager.instance.PlaySE("HasteBoots");
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
                AudioManager.instance.PlaySE("Squid");
                ikasumi = true;
                ikaRenderer.enabled = true;
                Invoke("ikasumiclean", 5.0f);
            }
            else
            {
                if (IsInvoking("ikasumiclean"))
                {
                    CancelInvoke("ikasumiclean");
                    Invoke("ikasumiclean", 5.0f);
                }
            }
        }
        if(collision.gameObject.CompareTag("skelton_item"))
        {
            AudioManager.instance.PlaySE("Skelton");
            EnemySend(collision.transform);
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
    private void EnemySend(Transform itemPos)
    {
        EnemySpawnManager.instance.RespawnEnemy(SendEnemyID, SendPlayerID, transform.position, EnemySpawnManager.instance.sendTrails[1]);
    }
}
