using ImTipsyDude.InstantECS;

public class SysResultView : IECSSystem
{
    private CmpResultView _cmpResultView;

    public override void OnStart()
    {
        _cmpResultView = GetComponent<CmpResultView>();
        var scoreMan = IniDScoreManager.Instance;

        _cmpResultView.CoinCountText.text = scoreMan.GotCoin.ToString("0000");
        // _cmpResultView.ScoreText.text =$"Score{scoreMan.Score}";
        // _cmpResultView.RemainTimeText.text =$"RemainTime:{scoreMan.RemainingTime.ToString("F1")}";
    }
}