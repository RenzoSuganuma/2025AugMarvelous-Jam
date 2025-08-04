using System;
using ImTipsyDude.IniD.Player;
using ImTipsyDude.InstantECS;
using UnityEngine;

public class SysPlayerOnCollision : IECSSystem
{
    private CmpIniDPlayer _cmp;
    
    public override void OnStart()
    {
        // _cmp = GetComponent<  >()
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            // スピードを止める
            
        }
    }
}