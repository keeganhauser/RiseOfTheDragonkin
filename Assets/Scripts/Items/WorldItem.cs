using UnityEngine;

/// <summary>
/// Represents an item that can be picked up in the game world
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class WorldItem : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Item item;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.ItemData.image;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Player.Instance.tag)
        {
            AudioSource.PlayClipAtPoint(
                AudioManager.Instance.pickupFX,
                this.transform.position,
                AudioManager.Instance.pickupVolume);
            InventoryManager.Instance.AddItem(item);
            Destroy(this.gameObject);
        }
    }
}
