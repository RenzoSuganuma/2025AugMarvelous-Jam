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

        private Vector3 _storedVelocity;

        private void OnStartDrag(Vector2 value)
        {
        }

        private void OnEndDrag(Unit _)
        {
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


            var w = GetEntity<EnPlayer>().World;
            _tipsyPlayerInput.OnJumpFired.Subscribe(_ => { w.PauseResume(); });
            w.OnPaused += OnPaused;
            w.OnResume += OnResume;
        }

        private void OnResume()
        {
            _rigidbody?.WakeUp();
            _rigidbody!.velocity = _storedVelocity;
        }

        private void OnPaused()
        {
            _storedVelocity = _rigidbody.velocity;
            _rigidbody?.Sleep();
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