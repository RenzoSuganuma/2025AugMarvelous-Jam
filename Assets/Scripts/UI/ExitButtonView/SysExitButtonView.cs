using ImTipsyDude.InstantECS;

public class SysExitButtonView : IECSSystem
{
    public override void OnStart()
    {
    }
    
    public void GotoTitle()
    {
        GetEntity<EnExitButtonView>().World.UnLoadScene("Exit", o => { });
    }
}