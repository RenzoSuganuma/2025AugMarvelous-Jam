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

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("JumpPlat"))
            {
                other.collider.isTrigger = true;

                _rigidbody.AddForce((Vector3.forward + Vector3.up).normalized
                                    * _cmpJumpAcceration.JumpForce,
                    ForceMode.Impulse);

                GetComponent<SysIniDPlayer>().SpeedUp(_cmpJumpAcceration.JumpForce);

                Destroy(other.gameObject, 1);
            }
        }
    }
}