using System;
using ImTipsyDude.InstantECS;
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
                _rigidbody.AddForce((Vector3.forward + Vector3.up) * _cmpJumpAcceration.JumpForce, ForceMode.Impulse);
            }
        }
    }
}