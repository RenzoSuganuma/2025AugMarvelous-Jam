using System.Collections;
using System.Collections.Generic;
using ImTipsyDude.InstantECS;
using UnityEngine;

public class SysSoundManager : IECSSystem
{
    private EnSoundManager _enSoundManager;
    private Dictionary<string, AudioClip> _bgmDict;
    private Dictionary<string, AudioClip> _seDict;

    public override void OnStart()
    {
        _enSoundManager = GetEntity<EnSoundManager>();
        _bgmDict = new Dictionary<string, AudioClip>();
        _seDict = new Dictionary<string, AudioClip>();

        foreach (var clip in _enSoundManager.bgmClips)
            _bgmDict[clip.name] = clip;

        foreach (var clip in _enSoundManager.seClips)
            _seDict[clip.name] = clip;
    }

    public void PlayBGM(string name)
    {
        if (_bgmDict.TryGetValue(name, out AudioClip clip))
        {
            _enSoundManager.bgmSource.clip = clip;
            _enSoundManager.bgmSource.loop = true;
            _enSoundManager.bgmSource.Play();
        }
    }

    public void StopBGM()
    {
        _enSoundManager.bgmSource.Stop();
    }

    public void PlaySE(string name)
    {
        if (_seDict.TryGetValue(name, out AudioClip clip))
        {
            _enSoundManager.seSource.PlayOneShot(clip);
        }
    }

    public void SetVolume(float bgmVolume, float seVolume)
    {
        _enSoundManager.bgmSource.volume = bgmVolume;
        _enSoundManager.seSource.volume = seVolume;
    }
}