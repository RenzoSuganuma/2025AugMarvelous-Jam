using System;
using ImTipsyDude.Helper;
using ImTipsyDude.InstantECS;

namespace ImTipsyDude.BolaBoom
{
    public class SysProgressView : IECSSystem
    {
        private CmpMonstoGuage _cpCmpMonstoGuage;

        public override void OnStart()
        {
            var se = GetEntity<EnProgressView>().World.CurrentScene;
            se.PullComponent(EnInstanceIdPool.Instance.Map[nameof(CmpMonstoGuage)], out _cpCmpMonstoGuage);
        }

        private void Update()
        {
            GetEntity<EnProgressView>().Text.text = (_cpCmpMonstoGuage.ProgressForUI * 100f).ToString("000");
        }
    }
}