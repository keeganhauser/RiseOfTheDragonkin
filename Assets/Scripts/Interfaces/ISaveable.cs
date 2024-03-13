public interface ISaveable
{
    public string ISaveableUniqueID { get; set; }
    GameObjectSave GameObjectSave { get; set; }

    public void ISaveableRegister();
    public void ISaveableUnregister();

    public void ISaveableStoreScene(string sceneName);
    public void ISaveableRestoreScene(string sceneName);

}
