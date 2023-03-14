using UnityEngine;

public class PlayerController : MonoBehaviour
{
    static float[] posLimits;

    void Start()
    {
        transform.localScale = DynamicGameSize.GetAppropriateSize(transform.localScale);
        if (posLimits == null)
        {
            float maxPosX = DynamicGameSize.camSize.x / 2 - transform.localScale.x / 2;
            float minPosX = -maxPosX;
            posLimits = new[] { minPosX, maxPosX };
        }
    }


    void FixedUpdate()
    {
        transform.position = new Vector3(GetValidPos(), transform.position.y);
    }

    float GetValidPos()
    {
        float inputPosX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        //check if in range
        if (inputPosX < posLimits[0])
            return posLimits[0];
        else if(posLimits[1] < inputPosX)
            return posLimits[1];

        return inputPosX;
    }
}
