using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class InteractableObject : MonoBehaviour
{
    [SerializeField] private delegate void InteractionDelegate();
    [SerializeField] private GameObject interactionDisplayPrefab;
    [SerializeField] private Vector2 interactionDisplayOffset;

    private GameObject interactionObj;

    private void Awake()
    {
        float height = GetComponent<SpriteRenderer>().sprite.bounds.size.y;

        // Add sprite child to object
        //interactionObj = Instantiate(
        //    interactionDisplayPrefab,
        //    new Vector3()
        //    {
        //        x = this.gameObject.transform.position.x + interactionDisplayOffset.x,
        //        y = this.gameObject.transform.position.y + interactionDisplayOffset.y + height,
        //        z = 0.0f
        //    },
        //    Quaternion.identity,
        //    this.gameObject.transform);
        //interactionObj.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != Player.Instance.tag) return;

        // Show interaction display
        interactionObj.SetActive(true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag != Player.Instance.tag) return;

        // Hide interaction display
        interactionObj.SetActive(false);
    }
}
