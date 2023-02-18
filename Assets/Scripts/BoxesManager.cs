using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BoxesManager : MonoBehaviour
{
    [SerializeField]
    private GameObject boxPrefab;

    [SerializeField]
    private ScoreHandler scoreHandler;

    private List<GameObject> boxes;

    private Vector2 camSize;

    private float moveAmount;
    private Vector3 moveVec;
    private float moved;

    Vector3[] boxSpawnPositions;

    void Start()
    {
        //cam size 5.625 10
        Camera cam = Camera.main;
        float aspectRatio = (float)Screen.width / (float)Screen.height;

        camSize = new Vector2(cam.orthographicSize * 2 * aspectRatio, cam.orthographicSize * 2);

        boxes = new List<GameObject>();

        moveAmount = camSize.x / 5;
        moveVec = new Vector2(0, camSize.x / 5 / 150);
        moved = moveAmount;

        boxSpawnPositions = new Vector3[5];

        for(int i = 0; i < boxSpawnPositions.Length; i++)
        {
            boxSpawnPositions[i] = new Vector3(camSize.x / 5 * i + camSize.x / 5 / 2 - camSize.x / 2, camSize.y / 2 + camSize.x / 5 / 2);
        }
    }

    void FixedUpdate()
    {
        if (boxes.Count == 0)
            GenerateBoxes();
        TryMoveBoxes();
    }

    private void GenerateBoxes()
    {
        int last;

        int SkipPosCount() => Random.Range(0, 3);
        int[] toSkip = GetRandomPosToSkip(SkipPosCount());

        for (int i = 0; i < boxSpawnPositions.Length; i++)
        {
            if (toSkip.Contains(i))
                continue;

            boxes.Add(Instantiate(boxPrefab));
            last = boxes.Count - 1;

            boxes[last].transform.position = boxSpawnPositions[i];

            boxes[last].GetComponent<OnDestroyActions>().PointsAddSet =
                () => { scoreHandler.AddPoints(10); };

            boxes[last].GetComponent<OnDestroyActions>().DispatcherSet = 
                (GameObject value) => { boxes.Remove(value); };

            //Makes box special
            bool isSpecial = IsBoxSpecial();
            boxes[last].GetComponent<Box>().specialBoxSet = isSpecial;

            if (isSpecial)
                boxes[last].GetComponent<OnDestroyActions>().DestroyEffectSet = GetComponent<EffectsController>().DoRandomEffect;
        }
        StartMoving();
    }

    private static int rNum() => Random.Range(0, 10);

    private bool IsBoxSpecial()
    {
        return rNum() == 0;
    }

    private int[] GetRandomPosToSkip(int count)
    {
        int[] toSkip = new int[count];
        int randomPos() => Random.Range(0, boxSpawnPositions.Length);
        int drawnNum;

        for(int i = 0; i < count;)
        {
            drawnNum = randomPos();
            if (!toSkip.Contains(drawnNum))
                toSkip[i++] = drawnNum;
        }

        return toSkip;
    }

    private void StartMoving()
    {
        moved = 0;
    }

    private void TryMoveBoxes()
    {
        bool CheckIfYouNeedToMove() => moved < moveAmount;

        if (CheckIfYouNeedToMove())
        {
            moved += moveVec.y;
            MoveBoxes();
        }
        else
        {
            GenerateBoxes();
        }
    }

    private void MoveBoxes()
    {
        foreach(var box in boxes)
        {
            box.transform.position -= moveVec;
        }
    }
}
