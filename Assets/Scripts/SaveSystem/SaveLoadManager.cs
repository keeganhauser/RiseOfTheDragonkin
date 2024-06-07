using MariaDBLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SaveLoadManager : SingletonMonoBehavior<SaveLoadManager>
{
    public List<ISaveable> iSaveableObjectList;
    private MariaDB db;

    protected override void Awake()
    {
        base.Awake();

        iSaveableObjectList = new List<ISaveable>();
        db = new MariaDB();
    }

    public void Save()
    {
        Player player = Player.Instance;
        db.Insert(
            player.Name, 
            player.transform.position.ToString("F6").Trim("()".ToCharArray()), 
            DateTime.Now, 
            SceneManager.GetActiveScene().name);
    }

    public void Load()
    {
        string data = db.Read(MariaDB.SelectType.MostRecent);
        string[] values = data.Split(';');

        string[] strCoords = values[2].Split(',');
        string sceneName = values[3].Trim();

        Vector2 pos = new Vector2(Convert.ToSingle(strCoords[0]), Convert.ToSingle(strCoords[1]));
        CustomSceneManager.Instance.FadeAndLoadScene(sceneName, pos);
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
