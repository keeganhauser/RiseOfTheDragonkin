using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrassTile : MonoBehaviour
{
    
    [SerializeField][Range(0f, 100f)] private float encounterPercentage = 20f;
    [SerializeField] private Enemy[] encounterableEnemies;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != Player.Instance.tag) return;

        if (Random.Range(0f, 100f) <= encounterPercentage)
        {
            Debug.Log("Start encounter!");
            CombatManager.Instance.StartEncounter(encounterableEnemies[Random.Range(0, encounterableEnemies.Length)]);
        }
        else
        {
            Debug.Log("Failed encounter");
        }
    }
}
