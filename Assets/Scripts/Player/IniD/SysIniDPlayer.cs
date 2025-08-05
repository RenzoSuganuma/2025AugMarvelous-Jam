using System;
using ImTipsyDude.Helper;
using ImTipsyDude.IniD.Constants;
using ImTipsyDude.InstantECS;
using ImTipsyDude.Scene;
using R3;
using UnityEngine;

namespace ImTipsyDude.IniD.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class SysIniDPlayer : IECSSystem
    {
        private TipsyPlayerInput _input;
        private CmpIniDPlayer _cmpIniDPlayer;
        private Rigidbody _rigidbody;
        private Vector3 _storedVelocity;
        private bool _isPaused = false;

        private void OnStartDrag(Vector2 v)
        {
        }

        private void OnEndDrag(Vector2 v)
        {
            var isRight = Vector2.Dot(Vector2.right, v) > 0;
            var isLeft = Vector2.Dot(-Vector2.right, v) > 0;

            // ドラッグ左右
            if (isRight)
            {
                transform.position += Vector3.right * IniDConstants.TileUnit;

                Debug.Log("Drag Right");
            }

            if (isLeft)
            {
                transform.position -= Vector3.right * IniDConstants.TileUnit;

                Debug.Log("Drag Left");
            }
        }

        public override void OnStart()
        {
            if (!gameObject.TryGetComponent(out _rigidbody))
            {
                _rigidbody = gameObject.AddComponent<Rigidbody>();
            }

            _input = TipsyPlayerInput.Instance;
            _input.OnStartDrag.Subscribe(OnStartDrag);
            _input.OnEndDrag.Subscribe(OnEndDrag);

            _cmpIniDPlayer = GetComponent<CmpIniDPlayer>();

            var w = GetEntity<EnIniDPlayer>().World;
            _input.OnJumpFired.Subscribe(_ =>
            {
                GetEntity<EnIniDPlayer>().World.CurrentScene.PullSystem(
                    EnInstanceIdPool.Instance.Map[nameof(SysGotCoinView)], out SysGotCoinView view);

                if (view.UseCoin())
                {
                    SpeedUp(300);
                }
            });
            w.OnPaused += OnPaused;
            w.OnResume += OnResume;

            w.GetSceneAs<Level1SceneEntity>().OnStartGame.Subscribe(_ =>
            {
                var go = GetEntity<EnIniDPlayer>().ActivateOnStart;
                foreach (var obj in go)
                {
                    obj.SetActive(true);
                }
            });

            EnInstanceIdPool.Instance.Map.Add(nameof(SysIniDPlayer), ID);
        }

        private void OnDestroy()
        {
            var w = GetEntity<EnIniDPlayer>().World;
            w.OnPaused -= OnPaused;
            w.OnResume -= OnResume;
        }

        private void OnResume()
        {
            _isPaused = false;
            _rigidbody?.WakeUp();
            _rigidbody!.velocity = _storedVelocity;
        }

        private void OnPaused()
        {
            _isPaused = true;
            _storedVelocity = _rigidbody.velocity;
            _rigidbody?.Sleep();
        }

        private void FixedUpdate()
        {
            if (_isPaused || GetEntity<EnIniDPlayer>().World.InGameState == InGameState.Waiting) return;

            _rigidbody.velocity = transform.forward * (_cmpIniDPlayer.CurrentMaxSpeed * Time.fixedDeltaTime);

            if (!Physics.Raycast(transform.position, Vector3.down, 0.5f, LayerMask.GetMask("Ground")))
            {
                _rigidbody.velocity += Vector3.down * 5;
            }

            if (transform.position.y < -5f)
            {
                transform.position += Vector3.up * 5;
            }
        }

        /// <summary> 指定した値スピードの値を上昇させる </summary>
        public void SpeedUp(float upValue)
        {
            _cmpIniDPlayer.CurrentMaxSpeed += upValue;
            _rigidbody.velocity = transform.forward * (_cmpIniDPlayer.CurrentMaxSpeed * Time.fixedDeltaTime);
        }

        public AudioSource GetSeSource()
        {
            return GetEntity<EnIniDPlayer>().SeSource;
        }
    }
}