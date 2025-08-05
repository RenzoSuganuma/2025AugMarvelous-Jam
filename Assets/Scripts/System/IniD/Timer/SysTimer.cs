using ImTipsyDude.IniD.Player;
using ImTipsyDude.InstantECS;
using UnityEngine;

namespace System.IniD.Timer
{
    public class SysTimer : IECSSystem
    {
        private CmpTimer _cmpTimer;
        private EnTimer _enTimer;
        private bool _isTimeOut = false;

        public override void OnStart()
        {
            _cmpTimer = GetComponent<CmpTimer>();
            _enTimer = GetEntity<EnTimer>();
        }

        public void Update()
        {
            if (GetEntity<EnTimer>().World.InGameState == InGameState.Waiting) return;

            if (_cmpTimer.TimeRemaining > 0)
            {
                _cmpTimer.TimeRemaining -= Time.deltaTime;
            }
            else if (!_isTimeOut && _cmpTimer.TimeRemaining <= 0)
            {
                _cmpTimer.TimeRemaining = 0f;
                _enTimer.OnTimeOut();
                _isTimeOut = true;
            }

            _enTimer.TimerText.text = _cmpTimer.TimeRemaining.ToString("0.00");
        }
    }
}