using ImTipsyDude.Helper;
using ImTipsyDude.IniD.Player;
using ImTipsyDude.InstantECS;
using TMPro;
using UnityEngine;

public class SysSpeedUI : IECSSystem
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

        var per = _speed / _cmpIniDPlayer.MaxSpeed;
        
        for (int i = 0; i < _enSpeedUI.Colors.Length; i++)
        {
            var f = (i + 1.0f) / _enSpeedUI.Colors.Length;
            if (f <= per)
            {
                _enSpeedUI.Cells[i].color = _enSpeedUI.Colors[i];
            }
            else
            {
                _enSpeedUI.Cells[i].color = Color.white;
            }
        }
    }

    private float GetSpeed()
    {
        return _cmpIniDPlayer.CurrentMaxSpeed;
    }
}