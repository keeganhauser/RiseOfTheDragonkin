using UnityEngine;

public class DrawRendererBounds : MonoBehaviour
{
    private SpriteRenderer _renderer;

    public void OnDrawGizmosSelected()
    {
        _renderer = GetComponent<SpriteRenderer>();
        Bounds bounds = _renderer.bounds;

        Gizmos.matrix = Matrix4x4.identity;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(bounds.center, bounds.extents * 2);
    }
}
