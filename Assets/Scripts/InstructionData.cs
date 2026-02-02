using UnityEngine;
using System;

public enum TaskType
{
    None,
    GrabScalpel,
    Cut,
    CutBezier,
    FlipSkin,
    GrabOrgan,
    PlaceInTray,
    ScanOrgan,
    RotatePage
}


[Serializable]
public class TaskInstruction
{
    //short instruction text
    [TextArea(1, 3)]
    public string taskDescription;

    public TaskType requiredTask;

    public string targetObjectName;

    //hint (arrow?)
    public GameObject hintObject;

    [HideInInspector]
    public bool isCompleted = false;
}

//one page (more tasks)
[Serializable]
public class Page
{
    //title
    [TextArea(3, 10)]
    public string pageTextHeader;

    public TaskInstruction[] tasks;
}