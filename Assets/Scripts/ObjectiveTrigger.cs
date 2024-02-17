using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[Serializable]
public class ObjectiveTrigger
{
    public Quest quest;
    public Quest.Status statusToApply;
    public int objectiveNumber;

    public void Invoke()
    {
        QuestManager manager = UnityEngine.Object.FindObjectOfType<QuestManager>();

        manager.UpdateObjectiveStatus(quest, objectiveNumber, statusToApply);
    }
}

[CustomPropertyDrawer(typeof(ObjectiveTrigger))]
public class ObjectiveTriggerDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty questProperty = property.FindPropertyRelative("quest");
        SerializedProperty statusProperty = property.FindPropertyRelative("statusToApply");
        SerializedProperty objectiveNumberProperty = property.FindPropertyRelative("objectiveNumber");

        int lineSpacing = 2;

        Rect firstLinePosition = position;
        firstLinePosition.height = base.GetPropertyHeight(questProperty, label);

        Rect secondLinePosition = position;
        secondLinePosition.y = firstLinePosition.y + firstLinePosition.height + lineSpacing;
        secondLinePosition.height = base.GetPropertyHeight(statusProperty, label);

        Rect thirdLinePosition = position;
        thirdLinePosition.y = secondLinePosition.y + secondLinePosition.height + lineSpacing;
        thirdLinePosition.height = base.GetPropertyHeight(objectiveNumberProperty, label);

        EditorGUI.PropertyField(firstLinePosition, questProperty, new GUIContent("Quest"));
        EditorGUI.PropertyField(secondLinePosition, statusProperty, new GUIContent("Status"));
        EditorGUI.PropertyField(thirdLinePosition, objectiveNumberProperty, new GUIContent("Objective"));

        Quest quest = questProperty.objectReferenceValue as Quest;

        if (quest != null && quest.objectives.Count > 0)
        {
            string[] objectiveNames = quest.objectives.Select(o => o.name).ToArray();
            int selectedObjective = objectiveNumberProperty.intValue;

            if (selectedObjective >= quest.objectives.Count)
            {
                selectedObjective = 0;
            }

            int newSelectedObjective = EditorGUI.Popup(thirdLinePosition, selectedObjective, objectiveNames);

            if (newSelectedObjective != selectedObjective)
            {
                objectiveNumberProperty.intValue = newSelectedObjective;
            }
        }
        else
        {
            using (new EditorGUI.DisabledGroupScope(true))
            {
                EditorGUI.Popup(thirdLinePosition, 0, new[] { "-" });
            }
        }
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int lineCount = 3;
        int lineSpacing = 2;
        float lineHeight = base.GetPropertyHeight(property, label);

        return (lineHeight * lineCount) + (lineSpacing * (lineCount - 1));
    }
}
