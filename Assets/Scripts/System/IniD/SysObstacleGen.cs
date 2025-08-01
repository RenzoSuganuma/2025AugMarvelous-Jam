using System;
using ImTipsyDude.IniD.Constants;
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
        }
    }
}