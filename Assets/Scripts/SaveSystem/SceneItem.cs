[System.Serializable]
public class SceneItem
{
    public int ItemCode;
    public Vector3Serializable Position;
    public string ItemName;

    public SceneItem()
    {
        Position = new Vector3Serializable();
    }
}
