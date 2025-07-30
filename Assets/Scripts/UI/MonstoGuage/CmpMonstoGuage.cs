using System;
using ImTipsyDude.Helper;
using ImTipsyDude.InstantECS;
using UnityEngine;

namespace ImTipsyDude.BolaBoom
{
    public class CmpMonstoGuage : IECSComponent
    {
        public int GuageMaxValue = 100;
        public float DurationToBeMax = 1f; // ドラッグ中にゲージMAXまで溜まる時間
        public float DelayToStartGuageUp = 1.5f; // ドラッグ中にゲージMAXまで溜まる時間
        public float Progress;
        public float ProgressForUI;

        public override void OnStart()
        {
            EnInstanceIdPool.Instance.Map.Add(nameof(CmpMonstoGuage), ID);
        }

        private void OnDestroy()
        {
            EnInstanceIdPool.Instance.Map.Remove(nameof(CmpMonstoGuage));
        }
    }
}