using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicZone : MonoBehaviour
{
    [SerializeField] GameState gameState;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != Player.Instance.tag) return;

        AudioManager.Instance.PlayMusic(gameState);
    }
}
