using System.Collections;
using System.Collections.Generic;
using ImTipsyDude.InstantECS;
using UnityEngine;

public class EnSoundManager : IECSEntity
{
    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource seSource;

    [Header("Audio Clips")]
    public AudioClip[] bgmClips;
    public AudioClip[] seClips;
}
