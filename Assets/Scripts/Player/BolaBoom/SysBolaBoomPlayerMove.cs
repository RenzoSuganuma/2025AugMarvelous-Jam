using System;
using Cysharp.Threading.Tasks;
using ImTipsyDude.Helper;
using ImTipsyDude.InstantECS;
using R3;
using Unity.Mathematics;
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

        private void OnEndDrag(Vector2 dir)
        {
            if (_ispaused) return;
            var guage = GetEntity<EnBolaBoomPlayer>().CmpMonstoGuage;
            var throwDir = -dir.normalized;
            var v = new Vector3(throwDir.x, 0, throwDir.y);
            _rigidbody.AddForce((v + Vector3.up).normalized
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

            EnInstanceIdPool.Instance.Map.Add(nameof(SysBolaBoomPlayerMove), ID);

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

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Ground")) return;

            var group = other.transform.GetComponentInParent<SysEnBolaObstacleGroup>();
            if (group != null)
            {
                group.Explode(transform.position);
                GetEntity<EnBolaBoomPlayer>().World.SlowMotion();
            }

            _rigidbody.AddExplosionForce(2000, transform.position, 100);
        }

        #endregion
    }
}