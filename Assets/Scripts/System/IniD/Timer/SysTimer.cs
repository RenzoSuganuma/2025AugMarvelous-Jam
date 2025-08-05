using DG.Tweening;
using ImTipsyDude.Helper;
using ImTipsyDude.IniD.Event;
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
        private bool _colorChanged = false;
        private bool _isBlinking =  false;

        public override void OnStart()
        {
            _cmpTimer = GetComponent<CmpTimer>();
            _enTimer = GetEntity<EnTimer>();
        }

        public void Update()
        {
            var world = GetEntity<EnTimer>().World;
            if (world.InGameState == InGameState.Waiting) return;

            if (!_colorChanged && _cmpTimer.TimeRemaining <= 10f)
            {
                _enTimer.TimerText.DOColor(Color.red, 1f);
                
                _colorChanged = true;
            }

            if (!_isBlinking && _cmpTimer.TimeRemaining <= 5f)
            {
                _enTimer.TimerText.DOFade(0.2f,0.4f)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutSine);
                
                _isBlinking = true;
            }

            if (_cmpTimer.TimeRemaining > 0)
            {
                _cmpTimer.TimeRemaining -= Time.deltaTime;
            }
            else if (!_isTimeOut && _cmpTimer.TimeRemaining <= 0)
            {
                _cmpTimer.TimeRemaining = 0f;
                world.UpdateInGameState(InGameState.Waiting);
                world.GetSceneAs<Level1SceneEntity>().OnTimeOut();

                // score
                IniDScoreManager.Instance.RemainingTime = _cmpTimer.TimeRemaining;

                IniDEventHandler.Instance.OnTimeOut?.Invoke();
                _isTimeOut = true;
            }

            _enTimer.TimerText.text = _cmpTimer.TimeRemaining.ToString("0.00");

            if (!_awaitingTimeUp && _cmpTimer.TimeRemaining <= 5f)
            {
                _enTimer.World.CurrentScene
                    .PullSystem(EnInstanceIdPool.Instance.Map[nameof(SysIniDPlayer)], out SysIniDPlayer p);

                var s = p.GetSeSource();

                SysSoundManager.Instance.PlaySE("SE_Finish Countdown", s);

                _awaitingTimeUp = true;
            }
        }
    }
}