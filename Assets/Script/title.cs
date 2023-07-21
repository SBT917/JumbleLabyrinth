using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class title : MonoBehaviour
{
    public float blinkInterval = 0.5f;  // ì_ñ≈ä‘äuÅiïbÅj
    private SpriteRenderer spriteRenderer;
    private float timer;
    private bool isImageActive;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        timer = 0f;
        isImageActive = true;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= blinkInterval)
        {
            isImageActive = !isImageActive;
            spriteRenderer.enabled = isImageActive;
            timer = 0f;
        }
    }
}
