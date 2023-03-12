using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public List<BallPack> ballPacks = new();
    public Vector3 playerPos;
    public int points;
    public BoxesManagerPack boxesManagerPack;
}

[Serializable]
public struct BallPack
{
    public BallPack(Vector3 pos, Vector3 moveAmount)
    {
        this.pos = pos;
        this.moveAmount = moveAmount;
    }

    public Vector3 pos;
    public Vector3 moveAmount;
}
[Serializable]
public struct BoxesManagerPack
{
    public BoxesManagerPack(List<GameObject> boxes, float moveAmount, Vector3 moveVec, float moved)
    {
        this.boxes = new();
        foreach (var box in boxes)
            this.boxes.Add(box.GetComponent<Box>().GetSaveData());
        
        this.moveAmount = moveAmount;
        this.moveVec = moveVec;
        this.moved = moved;
    }

    public List<BoxPack> boxes;

    public float moveAmount;
    public Vector3 moveVec;
    public float moved;
}

[Serializable]
public struct BoxPack
{
    public BoxPack(bool specialBox, Vector3 spawnPosition)
    {
        this.specialBox = specialBox;
        this.spawnPosition = spawnPosition;
    }

    public bool specialBox;
    public Vector3 spawnPosition;
}