using ImTipsyDude.Helper;
using ImTipsyDude.IniD.Event;
using ImTipsyDude.InstantECS;


public class SysGotCoinView : IECSSystem
{
    private EnGotCoinView _enCoinView;
    private CmpGotCoinView _cmpCoinView;

    public override void OnStart()
    {
        _enCoinView = GetEntity<EnGotCoinView>();
        _cmpCoinView = GetComponent<CmpGotCoinView>();

        EnInstanceIdPool.Instance.Map.Add(nameof(SysGotCoinView), ID);

        IniDEventHandler.Instance.OnTimeOut += () =>
        {
            IniDScoreManager.Instance.GotCoin = _cmpCoinView.GotCount;
        };
    }

    public void GotCoin()
    {
        _cmpCoinView.GotCount++;
        _enCoinView.Text.text = $"X{_cmpCoinView.GotCount}";
    }
}