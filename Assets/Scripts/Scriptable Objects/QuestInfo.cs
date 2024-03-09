using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Info", menuName = "Quest Info")]
public class QuestInfo : ScriptableObject
{
    [field: SerializeField] public string ID { get; private set; }

    [Header("General")]
    public string DisplayName;

    [Header("Requirements")]
    public int LevelRequirement;
    public QuestInfo[] QuestPrerequisites;

    [Header("Steps")]
    public GameObject[] QuestStepPrefabs;

    [Header("Rewards")]
    public int GoldReward;
    public int ExperienceReward;

    private void OnValidate()
    {
        #if UNITY_EDITOR
        ID = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }

}
