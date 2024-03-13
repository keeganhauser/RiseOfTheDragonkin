using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class LoadingZone : MonoBehaviour
{
    [SerializeField] private SceneName sceneToGoTo;
    [SerializeField] private Vector2 spawnLocation;

    private void Awake()
    {
        // Make sure the collider is set as a trigger
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.PlayerTag))
        {
            float x = Mathf.Approximately(spawnLocation.x, 0f) 
                ? Player.Instance.transform.position.x 
                : spawnLocation.x;

            float y = Mathf.Approximately(spawnLocation.y, 0f)
                ? Player.Instance.transform.position.y
                : spawnLocation.y;

            CustomSceneManager.Instance.FadeAndLoadScene(
                sceneToGoTo.ToString(),
                new Vector3(x, y, 0f));
        }
    }
}
