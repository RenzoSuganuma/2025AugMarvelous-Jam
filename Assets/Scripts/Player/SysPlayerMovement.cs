using Cysharp.Threading.Tasks;
using ImTipsyDude.InstantECS;
using R3;
using UnityEngine;

namespace ImTipsyDude.Player
{
    public class SysPlayerMovement : IECSSystem
    {
        private Rigidbody _rigidbody;
        private TipsyPlayerInput _tipsyPlayerInput;
        private CmpPlayer _cmpPlayer;

        private void OnStartDrag(Vector2 value)
        {
        }

        private void OnEndDrag(Unit _)
        {
            Debug.Log("OnEndDrag");
            var guage = GetEntity<EnPlayer>().CmpMonstoGuage;

            _rigidbody.AddForce((Vector3.forward + Vector3.up).normalized
                * _cmpPlayer.LaunchForce *
                guage.Progress / guage.GuageMaxValue,
                ForceMode.VelocityChange);
        }


        #region Event Functions

        public override void OnStart()
        {
            if (!gameObject.TryGetComponent(out _rigidbody))
            {
                _rigidbody = gameObject.AddComponent<Rigidbody>();
            }

            _cmpPlayer = GetComponent<CmpPlayer>();

            _tipsyPlayerInput = TipsyPlayerInput.Instance;
            _tipsyPlayerInput.OnStartDrag.Subscribe(OnStartDrag);
            _tipsyPlayerInput.OnEndDrag.Subscribe(OnEndDrag);
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

        #endregion
    }
}