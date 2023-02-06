using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxesManager : MonoBehaviour
{
    [SerializeField]
    private GameObject boxPrefab;

    private List<GameObject> boxes;

    private Vector2 camSize;
    void Start()
    {
        //cam size 5.625 10
        Camera cam = Camera.main;
        float aspectRatio = (float)Screen.width / (float)Screen.height;

        camSize = new Vector2(cam.orthographicSize * 2 * aspectRatio, cam.orthographicSize * 2);

        boxes = new List<GameObject>();
    }

    private void GenerateBoxes()
    {
        for (int i = 0; i < 5; i++)
        {
            boxes.Add(Instantiate(boxPrefab) as GameObject);
            boxes[i].transform.position = new Vector3(camSize.x / 5 * i + camSize.x / 5 / 2 - camSize.x / 2, camSize.y / 2 - camSize.x / 5 / 2);
            boxes[i].GetComponent<OnDestroyDispatcher>().Dispatcher = (GameObject value) => { boxes.Remove(value); };
        }
    }
    void FixedUpdate()
    {
        if (boxes.Count == 0)
            GenerateBoxes();
    }
}
