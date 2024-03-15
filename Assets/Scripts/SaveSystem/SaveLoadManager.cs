using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager : SingletonMonoBehavior<SaveLoadManager>
{
    public List<ISaveable> iSaveableObjectList;

    protected override void Awake()
    {
        base.Awake();

        iSaveableObjectList = new List<ISaveable>();
    }

    public void StoreCurrentSceneData()
    {
        foreach (ISaveable saveableObject in iSaveableObjectList)
        {
            saveableObject.ISaveableStoreScene(SceneManager.GetActiveScene().name);
        }
    }

    public void RestoreCurrentSceneData()
    {
        foreach (ISaveable saveableObject in iSaveableObjectList)
        {
            saveableObject.ISaveableRestoreScene(SceneManager.GetActiveScene().name);
        }
    }
}
