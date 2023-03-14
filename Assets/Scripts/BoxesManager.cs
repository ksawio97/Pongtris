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

    private float moveAmount;
    private Vector3 moveVec;
    private float moved;

    private float defaultMoveVec;
    private float maxMoveVec;

    static Vector3[] boxSpawnPositions;

    void Start()
    {
        defaultMoveVec = DynamicGameSize.camSize.y * DynamicGameSize.ratio.y / 10 / 150;
        maxMoveVec = defaultMoveVec * 2;

        if (boxes == null)
            SetStartVariables();

        if (boxSpawnPositions == null)
        {
            boxSpawnPositions = new Vector3[5];
            for (int i = 0; i < boxSpawnPositions.Length; i++)
                boxSpawnPositions[i] = new Vector3(DynamicGameSize.camSize.x / 5 * i + DynamicGameSize.camSize.x / 5 / 2 - DynamicGameSize.camSize.x / 2, DynamicGameSize.camSize.y / 2 + DynamicGameSize.camSize.x / 5 / 2);
        }
 
        //30s / 0.5s = 60
        StartCoroutine("BoostSpeed", (maxMoveVec - defaultMoveVec) / 60);
    }

    void SetStartVariables()
    {
        boxes = new();
        moveAmount = DynamicGameSize.camSize.x / 5;
        moveVec = new Vector2(0, defaultMoveVec);
        moved = moveAmount;
    }
    System.Collections.IEnumerator BoostSpeed(float moveBoost)
    {
        yield return new WaitForSeconds(0.5f);
        moveVec.y += moveBoost;
        if (moveVec.y < maxMoveVec)
        {
            StartCoroutine("BoostSpeed", moveBoost);

        }
    }
    void FixedUpdate()
    {
        if (boxes.Count == 0 && CheckSpawnsSafety())
            GenerateBoxes();
        TryMoveBoxes();
    }
    //to prevent getting a lot of free points
    bool CheckSpawnsSafety()
    {
        foreach(var spawnPosition in boxSpawnPositions)
        {
            Collider2D[] hitColliders = Physics2D.OverlapPointAll(spawnPosition);
            if (hitColliders.Select(x => x.tag).Contains("Explosion"))
                return false;
        }
        return true;
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

        for (int i = 0; i < count;)
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
        bool CheckNeedForAlignment() => moveAmount % moveVec.y != 0 && moved + moveVec.y > moveAmount;
        bool CheckIfYouNeedToMove() => moved < moveAmount;

        if (!CheckIfYouNeedToMove() || CheckNeedForAlignment())
            GenerateBoxes();
        else
        {
            moved += moveVec.y;
            MoveBoxes();
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
        CreateBox(boxPack.spawnPosition, boxPack.specialBox);
    }
}