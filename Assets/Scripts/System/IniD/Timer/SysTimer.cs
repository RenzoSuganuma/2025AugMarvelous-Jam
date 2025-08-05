using ImTipsyDude.Helper;
using ImTipsyDude.IniD.Player;
using ImTipsyDude.InstantECS;
using ImTipsyDude.Scene;
using UnityEngine;

namespace System.IniD.Timer
{
    public class SysTimer : IECSSystem
    {
        private CmpTimer _cmpTimer;
        private EnTimer _enTimer;
        private bool _isTimeOut = false;
        private bool _awaitingTimeUp = false;

        public override void OnStart()
        {
            _cmpTimer = GetComponent<CmpTimer>();
            _enTimer = GetEntity<EnTimer>();
        }

        public void Update()
        {
            var world = GetEntity<EnTimer>().World;
            if (world.InGameState == InGameState.Waiting) return;

            if (_cmpTimer.TimeRemaining > 0)
            {
                _cmpTimer.TimeRemaining -= Time.deltaTime;
            }
            else if (!_isTimeOut && _cmpTimer.TimeRemaining <= 0)
            {
                _cmpTimer.TimeRemaining = 0f;
                world.UpdateInGameState(InGameState.Waiting);
                world.GetSceneAs<Level1SceneEntity>().OnTimeOut();
                _enTimer.OnTimeOut?.Invoke();
                _isTimeOut = true;
            }

            _enTimer.TimerText.text = _cmpTimer.TimeRemaining.ToString("0.00");

            if (!_awaitingTimeUp && _cmpTimer.TimeRemaining <= 5f)
            {
                _enTimer.World.CurrentScene
                    .PullSystem( EnInstanceIdPool.Instance.Map[ nameof(SysIniDPlayer) ] , out SysIniDPlayer p );

                var s = p.GetSeSource();
                
                SysSoundManager.Instance.PlaySE("SE_Finish Countdown", s);
                
                _awaitingTimeUp = true;
            }
        }
    }
}