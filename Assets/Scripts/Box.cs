using UnityEngine;

public class Box : MonoBehaviour
{
    private SpriteRenderer spr;

    private bool _specialBox;

    public bool specialBoxSet { set { _specialBox = value; } }

    private float colorTimer = 0;

    void Start()
    {   
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
        if (_specialBox)
            SpecialColors();
    }
    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.transform.CompareTag("Explosion"))
            Destroy(gameObject);
    }

    private void OnCollisionExit2D(Collision2D coll)
    {
        Destroy(gameObject);
    }
}
