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
            audioSource = GetComponent<AudioSource>();
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

    [Header("Combat Music")]
    [SerializeField] private AudioClip combatMusic;
    [SerializeField][Range(0f, 1f)] private float combatMusicVolume;

    [Header("General FX")]
    public AudioClip pickupFX;
    [Range(0f, 1f)] public float pickupVolume;


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

    private AudioSource audioSource;

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
                audioSource.clip = menuMusic;
                audioSource.volume = mainMusicVolume;
                break;
            case GameState.Overworld:
                audioSource.clip = overworldMusic;
                audioSource.volume = overworldMusicVolume;
                break;
            case GameState.Combat:
                audioSource.clip = combatMusic;
                audioSource.volume = combatMusicVolume;
                break;
            default:
                break;
        }

        if (audioSource != null)
        {
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
