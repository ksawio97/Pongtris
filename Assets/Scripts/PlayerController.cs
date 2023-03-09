using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float[] posLimits;
    void Start()
    {
        Camera cam = Camera.main;
        float aspectRatio = (float)Screen.width / (float)Screen.height;

        Vector2 camSize = new Vector2(cam.orthographicSize * 2 * aspectRatio, cam.orthographicSize * 2);

        float maxPosX = camSize.x / 2 - transform.localScale.x / 2;
        float minPosX = -maxPosX;
        posLimits = new[] { minPosX, maxPosX };
    }


    void FixedUpdate()
    {
        transform.position = new Vector3(GetValidPos(), transform.position.y);
    }

    float GetValidPos()
    {
        float inputPosX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        //cjeck if in range
        if (inputPosX < posLimits[0])
            return posLimits[0];
        else if(posLimits[1] < inputPosX)
            return posLimits[1];

        return inputPosX;
    }
}
