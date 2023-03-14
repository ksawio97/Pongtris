using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

    private float defaultMoveVec;
    private float maxMoveVec;

    Vector3[] boxSpawnPositions;

    void Start()
    {
        Camera cam = Camera.main;
        float aspectRatio = (float)Screen.width / (float)Screen.height;

        camSize = new Vector2(cam.orthographicSize * 2 * aspectRatio, cam.orthographicSize * 2);
        defaultMoveVec = camSize.x / 5 / 150;

        //if didn't load
        if (boxes == null)
        {
            boxes = new ();
            moveAmount = camSize.x / 5;
            moveVec = new Vector2(0, defaultMoveVec);
            moved = moveAmount;
        }
        boxSpawnPositions = new Vector3[5];

        for(int i = 0; i < boxSpawnPositions.Length; i++)
            boxSpawnPositions[i] = new Vector3(camSize.x / 5 * i + camSize.x / 5 / 2 - camSize.x / 2, camSize.y / 2 + camSize.x / 5 / 2);

        maxMoveVec = defaultMoveVec * 2;
        //30s / 0.5s = 60
        StartCoroutine("BoostSpeed", (maxMoveVec - defaultMoveVec) / 60);
    }

    System.Collections.IEnumerator BoostSpeed(float moveBoost)
    {
        yield return new WaitForSeconds(0.5f);
        moveVec.y += moveBoost;
        if (moveVec.y < maxMoveVec)
        {
            StartCoroutine("BoostSpeed", moveBoost);

        }
        else
            Debug.Log($"Max Move Amount reached {moveVec.y}");
    }
    void FixedUpdate()
    {
        if (boxes.Count == 0)
            GenerateBoxes();
        TryMoveBoxes();
    }

    static int SkipPosCount() => Random.Range(0, 3);
    
    private void GenerateBoxes()
    {
        int[] toSkip = GetRandomPosToSkip(SkipPosCount());

        for (int i = 0; i < boxSpawnPositions.Length; i++)
        {
            if (toSkip.Contains(i))
                continue;

            CreateBox(boxSpawnPositions[i], IsBoxSpecial());
        }
        StartMoving();
    }

    private void CreateBox(Vector3 spawnPosition, bool specialBox)
    {
        boxes.Add(Instantiate(boxPrefab));

        int last = boxes.Count - 1;
        var boxScript = boxes[last].GetComponent<Box>();
        var onDestroyActionsScript = boxes[last].GetComponent<OnDestroyActions>();     

        boxes[last].transform.position = spawnPosition;

        onDestroyActionsScript.PointsAddSet =
            () => { scoreHandler.AddPoints(1); };

        onDestroyActionsScript.DispatcherSet =
            (GameObject value) => { boxes.Remove(value); };

        boxScript.specialBoxSet = specialBox;

        if (specialBox)
            onDestroyActionsScript.DestroyEffectSet = GetComponent<EffectsController>().DoRandomEffect;
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
        foreach (var box in boxes)
        {
            box.transform.position -= moveVec;
        }
    }

    public BoxesManagerPack GetSaveData()
    {
        return new BoxesManagerPack(
            boxes,
            moveAmount,
            moveVec,
            moved
        );
    }

    public void LoadSaveData(BoxesManagerPack boxesManagerPack)
    {
        boxes = new();
        foreach (var box in boxesManagerPack.boxes)
            LoadBoxSaveData(box);

        moveAmount = boxesManagerPack.moveAmount;
        moveVec = boxesManagerPack.moveVec;
        moved = boxesManagerPack.moved;
    }

    public void LoadBoxSaveData(BoxPack boxPack)
    {
        CreateBox(boxPack.spawnPosition , boxPack.specialBox);
    }
}
