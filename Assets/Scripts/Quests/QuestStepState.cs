using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestStepState
{
    public string State;
    public string Status;

    public QuestStepState() : this(string.Empty, string.Empty) { }

    public QuestStepState(string state, string status)
    {
        State = state;
        Status = status;
    }

}
