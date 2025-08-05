using ImTipsyDude.InstantECS;

public class SysResultView : IECSSystem
{
    private CmpResultView _cmpResultView;

    public override void OnStart()
    {
        _cmpResultView = GetComponent<CmpResultView>();
        var scoreMan = IniDScoreManager.Instance;

        _cmpResultView.CoinCountText.text = scoreMan.GotCoin.ToString();
        _cmpResultView.ScoreText.text = scoreMan.Score.ToString();
        _cmpResultView.RemainTimeText.text = scoreMan.RemainingTime.ToString("F1");
    }
}