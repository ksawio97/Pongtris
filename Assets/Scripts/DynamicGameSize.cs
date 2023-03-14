using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class DynamicGameSize
{
    public static Vector2 camSize;

    private static readonly Vector2 orginalCamSize = new (5.625f, 10);

    private static Vector2 _ratio;

    public static Vector2 ratio
    {
        get { return _ratio;  }
    }
    static DynamicGameSize()
    {
        Camera cam = Camera.main;
        float aspectRatio = (float)Screen.width / (float)Screen.height;
        camSize = new (cam.orthographicSize * 2 * aspectRatio, cam.orthographicSize * 2);

        _ratio = camSize / orginalCamSize;
    }

    public static Vector2 GetAppropriateSize(Vector2 inputSize)
    {
        return inputSize * ratio;
    }

    public static Vector3 GetAppropriateSize(Vector3 inputSize)
    {
        return new Vector3(inputSize.x * ratio.x, inputSize.y * ratio.y, inputSize.z);
    }
}
