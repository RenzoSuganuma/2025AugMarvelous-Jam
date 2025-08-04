using System;
using ImTipsyDude.IniD.Player;
using ImTipsyDude.InstantECS;
using Unity.VisualScripting;
using UnityEngine;

namespace Player.JumpAcceration
{
    public class SysJumpAcceration : IECSSystem
    {
        public CmpJumpAcceration _cmpJumpAcceration;
        private Rigidbody _rigidbody;

        public override void OnStart()
        {
            _rigidbody = gameObject.GetComponent<Rigidbody>();
        }

        private Quaternion _targetRotation;
        private bool _isAligning;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("JumpPlat"))
            {
                // 接触点の法線を取得
                Vector3 slopeNormal = other.contacts[0].normal;

                // 現在の forward を斜面法線に投影して、"進行方向" ベクトルを作る
                Vector3 projectedForward = Vector3.ProjectOnPlane(transform.forward, slopeNormal).normalized;

                // 斜面に沿った前方向＆上方向で回転を作成
                _targetRotation = Quaternion.LookRotation(projectedForward, slopeNormal);

                // 回転開始フラグ
                _isAligning = true;

                // 位置調整
                transform.position = new Vector3(0,other.contacts[0].point.y,other.contacts[0].point.z) + projectedForward * (transform.localScale.z / 2f);
            }
        }

        private void Update()
        {
            if (_isAligning)
            {
                // 滑らかに回転させる
                transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, _cmpJumpAcceration.AlignSpeed * Time.deltaTime);
                transform.position += Vector3.down * 0.03f;
                // ほぼ到達したら停止
                if (Quaternion.Angle(transform.rotation, _targetRotation) < 0.1f)
                {
                    transform.rotation = _targetRotation;
                    _isAligning = false;
                }
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("JumpPlat"))
            {
                GetComponent<SysIniDPlayer>().SpeedUp(_cmpJumpAcceration.JumpForce);
                //_rigidbody.AddForce(transform.forward * _cmpJumpAcceration.JumpForce, ForceMode.Impulse);
                //transform.up = Vector3.up;
                _isAligning = true;
                _targetRotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
                Debug.Log("OnCollisionExit");
            }
        }
    }
}