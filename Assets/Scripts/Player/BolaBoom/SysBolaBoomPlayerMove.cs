using Cysharp.Threading.Tasks;
using ImTipsyDude.InstantECS;
using R3;
using UnityEngine;

namespace ImTipsyDude.BolaBoom.Player
{
    public class SysBolaBoomPlayerMove : IECSSystem
    {
        private Rigidbody _rigidbody;
        private TipsyPlayerInput _tipsyPlayerInput;
        private CmpBolaBoomPlayer _cmpBolaBoomPlayer;

        private Vector3 _storedVelocity;
        private bool _ispaused = false;

        private void OnStartDrag(Vector2 value)
        {
        }

        private void OnEndDrag(Vector2 _)
        {
            if (_ispaused) return;
            var guage = GetEntity<EnBolaBoomPlayer>().CmpMonstoGuage;
            _rigidbody.AddForce((Vector3.forward + Vector3.up).normalized
                * _cmpBolaBoomPlayer.LaunchForce *
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

            _cmpBolaBoomPlayer = GetComponent<CmpBolaBoomPlayer>();

            _tipsyPlayerInput = TipsyPlayerInput.Instance;
            _tipsyPlayerInput.OnStartDrag.Subscribe(OnStartDrag);
            _tipsyPlayerInput.OnEndDrag.Subscribe(OnEndDrag);


            var w = GetEntity<EnBolaBoomPlayer>().World;
            _tipsyPlayerInput.OnJumpFired.Subscribe(_ => { w.PauseResume(); });
            w.OnPaused += OnPaused;
            w.OnResume += OnResume;
        }

        private void OnDestroy()
        {
            var w = GetEntity<EnBolaBoomPlayer>().World;
            w.OnPaused -= OnPaused;
            w.OnResume -= OnResume;
        }

        private void OnResume()
        {
            _ispaused = false;
            _rigidbody?.WakeUp();
            _rigidbody!.velocity = _storedVelocity;
        }

        private void OnPaused()
        {
            _ispaused = true;
            _storedVelocity = _rigidbody.velocity;
            _rigidbody?.Sleep();
        }

        #endregion
    }
}