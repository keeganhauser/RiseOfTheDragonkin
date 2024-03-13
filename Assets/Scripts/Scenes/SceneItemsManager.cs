//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[RequireComponent(typeof(GenerateGUID))]
//public class SceneItemsManager : SingletonMonobehavior<SceneItemsManager>, ISaveable
//{
//    private Transform parentItem;
//    [SerializeField] private GameObject itemPrefab;

//    private string iSaveableUniqueID;
//    public string ISaveableUniqueID
//    {
//        get => iSaveableUniqueID;
//        set => iSaveableUniqueID = value;
//    }

//    private GameObjectSave gameObjectSave;
//    public GameObjectSave GameObjectSave
//    {
//        get => gameObjectSave;
//        set => gameObjectSave = value;
//    }

//    protected override void Awake()
//    {
//        base.Awake();

//        ISaveableUniqueID = GetComponent<GenerateGUID>().GUID;
//        GameObjectSave = new GameObjectSave();
//    }

//    private void AfterSceneLoad()
//    {
//        parentItem = GameObject.FindGameObjectWithTag(Tags.ItemsParentTransform).transform;
//    }

//    private void DestroySceneItem()
//    {
//        // Get all items in scene
        
//    }
//}
