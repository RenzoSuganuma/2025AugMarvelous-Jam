using ImTipsyDude.Helper;
using ImTipsyDude.IniD.Player;
using ImTipsyDude.InstantECS;
using TMPro;

public class SysSpeedUI:IECSSystem
    {
        private EnSpeedUI _enSpeedUI;
        private CmpIniDPlayer　_cmpIniDPlayer;
        private float _speed;
        public override void OnStart()
        {
            _enSpeedUI = GetEntity<EnSpeedUI>();
            GetEntity<EnSpeedUI>().World.CurrentScene.PullComponent(
                EnInstanceIdPool.Instance.Map[nameof(CmpIniDPlayer)],
                out _cmpIniDPlayer
            );
        }

        private void Update()
        {
            _speed = GetSpeed();
            _enSpeedUI.PlayerSpeedText.text = $"速度：{_speed:F1}";
        }

        private float GetSpeed()
        {
            return _cmpIniDPlayer.CurrentMaxSpeed;
        }
    }
