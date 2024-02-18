using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private void InstantiateAudio()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != Instance)
        {
            Destroy(this.gameObject);
        }
    }

    // TODO: Move out of here
    public enum GameState
    {
        MainMenu,
        Overworld,
        Cave,
        Combat
    }

    [Header("Menu Music")]
    [SerializeField] private AudioClip menuMusic;
    [SerializeField][Range(0f, 1f)] private float mainMusicVolume;

    [Header("Overworld Music")]
    [SerializeField] private AudioClip overworldMusic;
    [SerializeField][Range(0f, 1f)] private float overworldMusicVolume;


    [Header("Test SFX")]
    [SerializeField] private AudioClip testSFX;
    [SerializeField][Range(0f, 1f)] private float testVolume;

    [SerializeField] private GameState startingGameState;

    private GameState currentGameState;
    public GameState CurrentGameState
    {
        get { return currentGameState; }
        set
        {
            currentGameState = value;
            PlayMusic();
        }
    }

    private void Awake()
    {
        InstantiateAudio();
        CurrentGameState = startingGameState;
    }

    private void PlayMusic()
    {
        switch (currentGameState)
        {
            case GameState.MainMenu:
                AudioSource.PlayClipAtPoint(menuMusic, Camera.main.transform.position, mainMusicVolume); 
                break;
            case GameState.Overworld:
                AudioSource.PlayClipAtPoint(overworldMusic, Camera.main.transform.position, overworldMusicVolume);
                break;
            default:
                break;
        }
    }

    public void PlayOneShot()
    {

    }
}
