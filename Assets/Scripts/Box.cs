using UnityEngine;

public class Box : MonoBehaviour
{
    private SpriteRenderer spr;

    private bool _specialBox;

    public bool specialBoxSet { set { _specialBox = value; } }

    private float _colorTimer;

    void Start()
    {
        _colorTimer = 0;
        spr = GetComponent<SpriteRenderer>();
        spr.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    private void SpecialColors()
    {
        _colorTimer += Time.deltaTime;

        if (_colorTimer >= 1f)
            _colorTimer = 0;

        spr.color = Color.HSVToRGB(_colorTimer, 1, 1);
    }

    private void FixedUpdate()
    {
        if (_specialBox)
            SpecialColors();
    }

    public BoxPack GetSaveData()
    {
        return new BoxPack(
            _specialBox,
            transform.position
        );
    }
}
