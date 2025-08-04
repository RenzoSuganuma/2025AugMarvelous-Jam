using DG.Tweening;
using ImTipsyDude.Helper;
using ImTipsyDude.InstantECS;
using UnityEngine;

public class SysPlayerShuriken : IECSSystem
{
    private EnPlayerShuriken _enPlayerShuriken;
    private CmpPlayerShuriken _cmpPlayerShuriken;

    public override void OnStart()
    {
        _enPlayerShuriken = GetEntity<EnPlayerShuriken>();
        _cmpPlayerShuriken = GetComponent<CmpPlayerShuriken>();

        PlaySmoke();

        _enPlayerShuriken.SparkParticles.SetActive(false);
        _enPlayerShuriken.BuffParticle.SetActive(false);
        
        EnInstanceIdPool.Instance.Map.Add(nameof(SysPlayerShuriken), ID);
    }

    public void PlaySmoke(float duration = 0)
    {
        _enPlayerShuriken.SmokeParticles.SetActive(true);

        if (duration > 0)
        {
            DOTween.To(() => 1, value => { }, 1, duration)
                .OnComplete(() => { StopSmoke(); });
        }
    }

    public void PlaySpark()
    {
        _enPlayerShuriken.SparkParticles.SetActive(true);

        var duration = _cmpPlayerShuriken.JumpingDuration;

        if (duration > 0)
        {
            DOTween.To(() => 1, value => { }, 1, duration)
                .OnComplete(() => { _enPlayerShuriken.SparkParticles.SetActive(false); });
        }
    }

    public void PlayBuff()
    {
        _enPlayerShuriken.BuffParticle.SetActive(true);

        var duration = _cmpPlayerShuriken.BuffDuration;

        if (duration > 0)
        {
            DOTween.To(() => 1, value => { }, 1, duration)
                .OnComplete(() => { _enPlayerShuriken.BuffParticle.SetActive(false); });
        }
    }

    public void StopSmoke()
    {
        _enPlayerShuriken.SmokeParticles.SetActive(false);
    }
}