using ImTipsyDude.InstantECS;

namespace ImTipsyDude.Scene
{
    public class Level1SceneEntity : SceneEntity
    {
        public override void OnStart()
        {
            IECSWorld.Instance.StartLoadNextScene("Exit");
            IECSWorld.Instance.UpdateInGameState(InGameState.Playing);
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