using System;
using ImTipsyDude.IniD.Constants;
using ImTipsyDude.IniD.Player;
using ImTipsyDude.InstantECS;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

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
            
            GenerateObstacle();
        }

        private void GenerateObstacle()
        {
            var cmp = _enGen.GetComponent<CmpObstacleGen>();
            var basepos = transform.position + (Vector3.forward * cmp.SpawnDistance);
            var genLineStart = basepos + (Vector3.left * cmp.ColliderSize.x);
            var genLineEnd = basepos + (Vector3.right * cmp.ColliderSize.x);

            var genPos = Vector3.Lerp(genLineStart, genLineEnd, Random.Range(0.0f, 1.0f));

            Instantiate(_enGen.Obstacles[Random.Range(0, _enGen.Obstacles.Length)], genPos, Quaternion.identity);
        }
    }
}