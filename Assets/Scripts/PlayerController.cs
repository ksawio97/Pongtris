using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float[] posLimits;
    void Start()
    {
        Debug.Log(Camera.main.orthographicSize);
        float maxPosX = Camera.main.orthographicSize / 2 - GetComponent<BoxCollider2D>().size.x / 2;
        float minPosX = -maxPosX;
        posLimits = new []{minPosX, maxPosX};
    }


    void FixedUpdate()
    {
        transform.position = new Vector3(GetValidPos(), transform.position.y);
        Debug.Log(UnityEditor.EditorApplication.isRemoteConnected);
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
