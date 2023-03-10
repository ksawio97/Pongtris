using UnityEngine;

public class SetBorders : MonoBehaviour
{
    private static readonly float additionalSpace = 0.5f;

    [SerializeField]
    private BoxCollider2D[] Borders;

    [SerializeField]
    private BoxCollider2D BallArea;

    void Start()
    {
        Camera cam = Camera.main;
        float aspectRatio = (float)Screen.width / (float)Screen.height;

        Vector2 camSize = new Vector2(cam.orthographicSize * 2 * aspectRatio, cam.orthographicSize * 2);

        CreateBorders(camSize);
        SetupBallArea(camSize);
    }

    private void SetupBallArea(Vector2 camSize)
    {
        var area = BallArea.GetComponent<BoxCollider2D>();
        area.size = camSize;
    }
    private void CreateBorders(Vector2 camSize)
    {
        //position and sizes of colliders
        Vector3[][] args =
        {
            new []{ new Vector3(-(camSize.x / 2 + additionalSpace), 0), new Vector3(additionalSpace * 2, camSize.y)},
            new []{ new Vector3(camSize.x / 2 + additionalSpace, 0), new Vector3(additionalSpace * 2, camSize.y)},
            new []{ new Vector3(0, camSize.y / 2 + additionalSpace), new Vector3(camSize.x * 1.5f, additionalSpace * 2)}
        };

        for (int i = 0; i < Borders.Length; i++)
            AdjustBorder(Borders[i], args[i]);
    }

    private static void AdjustBorder(BoxCollider2D Border, Vector3[] PosSizePair)
    {
        Border.transform.position = PosSizePair[0];
        Border.size = PosSizePair[1];
    }
}
