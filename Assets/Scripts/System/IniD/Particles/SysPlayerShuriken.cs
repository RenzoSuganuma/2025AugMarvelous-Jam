using ImTipsyDude.InstantECS;
using UnityEngine;

public class SysPlayerShuriken : IECSSystem
{
    private EnPlayerShuriken _enPlayerShuriken;

    public override void OnStart()
    {
        _enPlayerShuriken = GetEntity<EnPlayerShuriken>();
        PlaySmoke();
        StopSpark();
        StopBuff();
    }

    public void PlaySmoke()
    {
        _enPlayerShuriken.SmokeParticles.SetActive(true);
    }

    public void PlaySpark()
    {
        _enPlayerShuriken.SparkParticles.SetActive(true);
    }

    public void PlayBuff()
    {
        _enPlayerShuriken.BuffParticle.SetActive(true);
    }

    public void StopSmoke()
    {
        _enPlayerShuriken.SmokeParticles.SetActive(false);
    }

    public void StopSpark()
    {
        _enPlayerShuriken.SparkParticles.SetActive(false);
    }

    public void StopBuff()
    {
        _enPlayerShuriken.BuffParticle.SetActive(false);
    }
}