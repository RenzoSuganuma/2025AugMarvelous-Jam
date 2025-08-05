using System;
using ImTipsyDude.Helper;
using ImTipsyDude.IniD.Event;
using ImTipsyDude.InstantECS;

public class SysScoreView : IECSSystem
{
    private CmpScoreView _cmpScoreView;

    public override void OnStart()
    {
        _cmpScoreView = GetComponent<CmpScoreView>();

        EnInstanceIdPool.Instance.Map.Add(nameof(SysScoreView), ID);

        IniDEventHandler.Instance.OnTimeOut += () =>
        {
            IniDScoreManager.Instance.Score = _cmpScoreView.Score;
        };

        UpdateScore();
    }

    public void AddScore(int score)
    {
        _cmpScoreView.Score += score;
        UpdateScore();
    }

    public void UpdateScore()
    {
        _cmpScoreView._text.text = _cmpScoreView.Score.ToString("000000000");
    }
}