using DG.Tweening;
using ImTipsyDude.InstantECS;
using UnityEngine;

namespace ImTipsyDude.Scene
{
    public class Level1SceneEntity : SceneEntity
    {
        public override void OnStart()
        {
            IECSWorld.Instance.StartLoadNextScene("Exit");
            IECSWorld.Instance.UpdateInGameState(InGameState.Waiting);

            DOTween.Sequence()
                .AppendCallback(() => Debug.Log("3"))
                .AppendInterval(0.5f)
                .AppendCallback(() => Debug.Log("2"))
                .AppendInterval(0.5f)
                .AppendCallback(() => Debug.Log("1"))
                .AppendInterval(0.5f)
                .AppendCallback(() => IECSWorld.Instance.UpdateInGameState(InGameState.Playing)).Play();
        }

        public override void OnUpdate()
        {
        }

        public override void OnFixedUpdate()
        {
        }

        public override void OnTerminate()
        {
        }
    }
}