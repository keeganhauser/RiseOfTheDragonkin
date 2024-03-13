using UnityEngine;

[ExecuteAlways]
public class GenerateGUID : MonoBehaviour
{
    [SerializeField] private string guid;

    public string GUID 
    {
        get => guid;
        set => guid = value; 
    }

    private void Awake()
    {
        // Only populate in the editor
        if (!Application.IsPlaying(gameObject))
        {
            if (string.IsNullOrEmpty(guid))
            {
                guid = System.Guid.NewGuid().ToString();
            }
        }
    }
}
