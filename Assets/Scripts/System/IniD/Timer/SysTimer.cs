using ImTipsyDude.IniD.Player;
using ImTipsyDude.InstantECS;
using UnityEngine;

namespace System.IniD.Timer
{
    public class SysTimer: IECSSystem
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
            _cmpTimer.TimeRemaining -= Time.deltaTime;
            _enTimer.TimerText.text = _cmpTimer.TimeRemaining.ToString("0.00");

            if (!_isTimeOut && _cmpTimer.TimeRemaining <= 0)
            {
                _enTimer.OnTimeOut();
                _isTimeOut = true;
            }
        }
        
        
    }
}