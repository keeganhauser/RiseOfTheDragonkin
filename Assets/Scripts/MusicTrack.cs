using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Music Track", menuName = "Scriptable object/Music Track")]
public class MusicTrack : ScriptableObject
{
    public AudioClip audioClip;
    [Range(0f, 1f)] public float defaultVolume;
}
