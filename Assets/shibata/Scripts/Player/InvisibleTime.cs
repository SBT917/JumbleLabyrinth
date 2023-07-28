using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class InvisibleTime : MonoBehaviour, IInvisiblable
{
    [SerializeField]float invTime = 2.0f;
    bool isInvisible;

    public bool IsInvisible { get => isInvisible; }
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        TryGetComponent(out spriteRenderer);
    }

    public void Invisible()
    {
        StartCoroutine(InvisibleCoroutine());
        StartCoroutine(FlashCoroutine());
    }

    IEnumerator InvisibleCoroutine()
    {
        isInvisible = true;
        yield return new WaitForSeconds(invTime);
        isInvisible = false;
    }

    IEnumerator FlashCoroutine()
    {
        while (isInvisible)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }


}
