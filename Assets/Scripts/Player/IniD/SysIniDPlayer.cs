using System;
using ImTipsyDude.InstantECS;
using R3;
using UnityEngine;

namespace ImTipsyDude.IniD.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class SysIniDPlayer : IECSSystem
    {
        private const float TileUnit = 1.0f;
        
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
                transform.position += Vector3.right * TileUnit;
                
                Debug.Log("Drag Right");
            }

            if (isLeft)
            {
                transform.position -= Vector3.right * TileUnit;
                
                Debug.Log("Drag Left");
            }
        }

        public override void OnStart()
        {
            if (!gameObject.TryGetComponent(out _rigidbody))
            {
                _rigidbody = gameObject.AddComponent<Rigidbody>();
            }

            var input = TipsyPlayerInput.Instance;
            input.OnStartDrag.Subscribe(OnStartDrag);
            input.OnEndDrag.Subscribe(OnEndDrag);

            _cmpIniDPlayer = GetComponent<CmpIniDPlayer>();
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = transform.forward * (_cmpIniDPlayer.MaxSpeed * Time.fixedDeltaTime);
        }
    }
}