using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Anima : MonoBehaviour
{
   

    public Sprite[] sprites;
    public float animationTime = 0.25f;

    private SpriteRenderer spriteRenderer;
    private float timer;
    private int currentIndex;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[0];
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < animationTime)
        {
            return;
        }

        spriteRenderer.sprite = sprites[currentIndex++ % sprites.Length];
        timer = 0f;
    }
}

