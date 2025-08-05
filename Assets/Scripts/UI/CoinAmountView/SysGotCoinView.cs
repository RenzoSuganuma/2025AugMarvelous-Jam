using DG.Tweening;
using ImTipsyDude.Helper;
using ImTipsyDude.IniD.Event;
using ImTipsyDude.InstantECS;
using UnityEngine;


public class SysGotCoinView : IECSSystem
{
    private EnGotCoinView _enCoinView;
    private CmpGotCoinView _cmpCoinView;
    private Tweener _tween;

    public override void OnStart()
    {
        _enCoinView = GetEntity<EnGotCoinView>();
        _cmpCoinView = GetComponent<CmpGotCoinView>();

        EnInstanceIdPool.Instance.Map.Add(nameof(SysGotCoinView), ID);

        IniDEventHandler.Instance.OnTimeOut += () => { IniDScoreManager.Instance.GotCoin = _cmpCoinView.GotCount; };
    }

    public void GotCoin()
    {
        _cmpCoinView.GotCount++;
        _enCoinView.Text.text = $"X{_cmpCoinView.GotCount}";
    }

    public bool UseCoin()
    {
        var b = _cmpCoinView.GotCount - 1 > 0;
        if (b)
        {
            _cmpCoinView.GotCount--;
            _enCoinView.Text.text = $"X{_cmpCoinView.GotCount}";
        }

        return b;
    }
}