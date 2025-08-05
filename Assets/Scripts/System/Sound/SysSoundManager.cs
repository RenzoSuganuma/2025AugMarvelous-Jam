using System.Collections;
using System.Collections.Generic;
using ImTipsyDude.Helper;
using ImTipsyDude.InstantECS;
using UnityEngine;

public class SysSoundManager : MonoBehaviour
{
    public static SysSoundManager Instance { get; private set; }

    private EnSoundManager _enSoundManager;
    private Dictionary<string, AudioClip> _bgmDict = new();
    private Dictionary<string, AudioClip> _seDict = new();

    public void Start()
    {
        _enSoundManager = GetComponent<EnSoundManager>();

        foreach (var clip in _enSoundManager.bgmClips)
        {
            _bgmDict[clip.name.ToLower()] = clip;
        }

        foreach (var clip in _enSoundManager.seClips)
        {
            _seDict[clip.name.ToLower()] = clip;
        }

        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PlayBGM(string name)
    {
        if (_bgmDict.TryGetValue(name.ToLower(), out AudioClip clip))
        {
            _enSoundManager.bgmSource.clip = clip;
            _enSoundManager.bgmSource.loop = true;
            _enSoundManager.bgmSource.Play();
        }
    }

    public void PlayBGM(string name, AudioSource source)
    {
        if (_bgmDict.TryGetValue(name.ToLower(), out AudioClip clip))
        {
            source.clip = clip;
            source.loop = true;
            source.Play();
        }
    }

    public void StopBGM()
    {
        _enSoundManager.bgmSource.Stop();
    }

    public void PlaySE(string name)
    {
        if (_seDict.TryGetValue(name.ToLower(), out AudioClip clip))
        {
            _enSoundManager.seSource.PlayOneShot(clip);
        }
    }

    public void PlaySE(string name, AudioSource source)
    {
        if (_seDict.TryGetValue(name.ToLower(), out AudioClip clip))
        {
            source.PlayOneShot(clip);
        }
    }

    public void SetVolume(float bgmVolume, float seVolume)
    {
        _enSoundManager.bgmSource.volume = bgmVolume;
        _enSoundManager.seSource.volume = seVolume;
    }
}