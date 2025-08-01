using System;
using ImTipsyDude.IniD.Constants;
using ImTipsyDude.IniD.Player;
using ImTipsyDude.InstantECS;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

namespace ImTipsyDude.System.IniD
{
    [RequireComponent(typeof(BoxCollider))]
    public class SysObstacleGen : IECSSystem
    {
        private EnObstacleGen _enGen;

        public override void OnStart()
        {
            _enGen = GetEntity<EnObstacleGen>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var v = other.GetComponent<CmpIniDPlayer>();
            var apply = Math.Clamp(v.CurrentMaxSpeed * 2.0f, 0, v.MaxSpeed);
            v.CurrentMaxSpeed = apply;
            
            transform.position += Vector3.forward * 10;
        }
    }
}