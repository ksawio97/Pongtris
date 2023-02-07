using Assets.Scripts;
using System;
using UnityEngine;

public class Box : MonoBehaviour
{
    private bool specialBox;
    private float colorTimer = 0;

    private SpriteRenderer spr;

    void Start()
    {
        //0 - 9 
        int rNum() => UnityEngine.Random.Range(0, 10);

        specialBox = rNum() == 0;

        spr = GetComponent<SpriteRenderer>();
        spr.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    private void SpecialColors()
    {
        colorTimer += Time.deltaTime;

        if (colorTimer >= 1f)
        {
            colorTimer = 0;
        }

        spr.color = Color.HSVToRGB(colorTimer, 1, 1);
    }

    private void FixedUpdate()
    {
        if (specialBox)
            SpecialColors();
    }

    private void OnDestroy()
    {
        if (specialBox)
            EffectsController.DoRandomEffect();
    }

    private void OnCollisionExit2D(Collision2D coll)
    {
        Destroy(gameObject);
    }
}
