using System;
using ImTipsyDude.Helper;
using ImTipsyDude.IniD.Player;
using ImTipsyDude.InstantECS;
using UnityEngine;

namespace Player.JumpAcceration
{
    public class SysJumpAcceration : IECSSystem
    {
        private CmpJumpAcceration _cmpJumpAcceration;
        private Rigidbody _rigidbody;

        public override void OnStart()
        {
            _rigidbody = gameObject.GetComponent<Rigidbody>();
            _cmpJumpAcceration = GetComponent<CmpJumpAcceration>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("JumpPlat"))
            {
                other.collider.isTrigger = true;

                _rigidbody.AddForce((Vector3.forward + Vector3.up).normalized
                                    * _cmpJumpAcceration.JumpForce,
                    ForceMode.Impulse);

                var s = GetEntity<EnJumpAcceration>().World.CurrentScene;
                s.PullSystem(EnInstanceIdPool.Instance.Map[nameof(SysPlayerShuriken)],
                    out SysPlayerShuriken particle);
                particle.PlaySpark();
                GetComponent<SysIniDPlayer>().SpeedUp(_cmpJumpAcceration.JumpForce);

                Destroy(other.gameObject, 1);

                SysSoundManager.Instance.PlaySE("SE_Acceleration", GetEntity<EnJumpAcceration>().SeSource);
                //なんかうまく動かなかった。Keyが登録されてないとか
                // var sl = GetEntity<EnSyutyuLine>().World.CurrentScene;
                // sl.PullSystem(EnInstanceIdPool.Instance.Map[nameof(SysSyutyuLine)],
                //     out SysSyutyuLine line);
                // line.Syutyu();
                GetComponent<SysSyutyuLine>().Syutyu();
            }
        }
    }
}