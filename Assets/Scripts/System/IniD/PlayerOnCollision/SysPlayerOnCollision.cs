using System;
using ImTipsyDude.IniD.Player;
using ImTipsyDude.InstantECS;
using UnityEngine;

public class SysPlayerOnCollision : IECSSystem
{
    [SerializeField] private float _regainSpeed;
    private CmpIniDPlayer _cmp;

    public override void OnStart()
    {
        _cmp = GetComponent<CmpIniDPlayer>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            // スピードを止める
            _cmp.CurrentMaxSpeed = 0;
            Debug.Log("障害物に当たりました");
        }
    }
    
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            // スピードを止める
            _cmp.CurrentMaxSpeed = _regainSpeed;
            Debug.Log("障害物に当たりました");
        }
    }
}