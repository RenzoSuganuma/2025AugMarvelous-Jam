using System.Collections;
using System.Collections.Generic;
using ImTipsyDude.InstantECS;
using UnityEngine;

public class SysSoundManager : IECSSystem
{
    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource seSource;

    [Header("Audio Clips")]
    public AudioClip[] bgmClips;
    public AudioClip[] seClips;

    private Dictionary<string, AudioClip> bgmDict;
    private Dictionary<string, AudioClip> seDict;

    public override void OnStart()
    {
        bgmDict = new Dictionary<string, AudioClip>();
        seDict = new Dictionary<string, AudioClip>();

        foreach (var clip in bgmClips)
            bgmDict[clip.name] = clip;

        foreach (var clip in seClips)
            seDict[clip.name] = clip;
    }

    public void PlayBGM(string name)
    {
        if (bgmDict.TryGetValue(name, out AudioClip clip))
        {
            bgmSource.clip = clip;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlaySE(string name)
    {
        if (seDict.TryGetValue(name, out AudioClip clip))
        {
            seSource.PlayOneShot(clip);
        }
    }

    public void SetVolume(float bgmVolume, float seVolume)
    {
        bgmSource.volume = bgmVolume;
        seSource.volume = seVolume;
    }
}