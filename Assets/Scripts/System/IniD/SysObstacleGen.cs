using System;
using ImTipsyDude.Helper;
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
        private CmpIniDPlayer _cmpPlayer;

        public override void OnStart()
        {
            _enGen = GetEntity<EnObstacleGen>();

            GetEntity<EnObstacleGen>().World.CurrentScene.PullComponent(
                EnInstanceIdPool.Instance.Map[nameof(CmpIniDPlayer)], out _cmpPlayer);
        }

        private void OnTriggerEnter(Collider other)
        {
            var apply = Math.Clamp(_cmpPlayer.CurrentMaxSpeed * 2.0f, 0, _cmpPlayer.MaxSpeed);
            _cmpPlayer.CurrentMaxSpeed = apply;

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