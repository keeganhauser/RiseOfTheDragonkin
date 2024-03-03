using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu,
    Overworld,
    Cave,
    Combat
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private const float fadeDuration = 1f;

    private void InstantiateAudio()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    [Header("Menu Music")]
    [SerializeField] private MusicTrack[] menuTracks;

    [Header("Overworld Music")]
    [SerializeField] private MusicTrack[] overworldTracks;

    [Header("Combat Music")]
    [SerializeField] private MusicTrack[] combatTracks;

    [Header("General FX")]
    public AudioClip pickupFX;
    [Range(0f, 1f)] public float pickupVolume;


    [SerializeField] private GameState startingGameState;
    public GameState CurrentGameState { get; set; }

    private AudioSource audioSource;
    private MusicTrack[] currentTrackList;
    private int currentTrackIndex;

    private void Awake()
    {
        InstantiateAudio();
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;

        CurrentGameState = startingGameState;
        UpdateTrackList(CurrentGameState);
    }

    private void Update()
    {
        if (audioSource.isPlaying) return;

        PlayTrack(currentTrackIndex++);
    }

    public void UpdateTrackList(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MainMenu:
                currentTrackList = menuTracks;
                break;
            case GameState.Overworld:
                currentTrackList = overworldTracks;
                break;
            case GameState.Combat:
                currentTrackList = combatTracks;
                break;
            default:
                break;
        }

        PlayTrack(0);
    }

    public void PlayMusic(GameState gameState)
    {

    }

    private void PlayTrack(int index)
    {
        if (index < 0 || index >= currentTrackList.Length) return;

        // Start to fade out current song
        //StartCoroutine(FadeOut(0.5f));

        currentTrackIndex = index;
        audioSource.clip = currentTrackList[currentTrackIndex].audioClip;
        audioSource.volume = currentTrackList[currentTrackIndex].defaultVolume;

        StartCoroutine(FadeIn(fadeDuration));
    }

    public IEnumerator FadeOut(float duration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0) 
        {
            audioSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
        
    }

    public IEnumerator FadeIn(float duration)
    {
        float startVolume = audioSource.volume;

        audioSource.volume = 0;
        audioSource.Play();

        while (audioSource.volume < startVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / duration;
            yield return null;
        }

        audioSource.volume = startVolume;
    }
}
