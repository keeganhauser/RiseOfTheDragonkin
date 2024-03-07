using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObjectiveOnInteraction : MonoBehaviour
{
    [SerializeField]
    private ObjectiveTrigger objective = new ObjectiveTrigger();

    private void Awake()
    {
        GetComponent<NPC>()?.InteractionEvent.AddListener(objective.Invoke);
    }
}
