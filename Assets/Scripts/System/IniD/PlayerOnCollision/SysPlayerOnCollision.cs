using System;
using ImTipsyDude.IniD.Player;
using ImTipsyDude.InstantECS;
using UnityEngine;

public class SysPlayerOnCollision : IECSSystem
{
    private CmpIniDPlayer _cmp;
    private CmpPlayerOnCollision _cmpPlayerOnCollision;

    public override void OnStart()
    {
        _cmp = GetComponent<CmpIniDPlayer>();
        _cmpPlayerOnCollision = GetComponent<CmpPlayerOnCollision>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            // スピードを止める
            _cmp.CurrentMaxSpeed = 0;
            Debug.Log("障害物に当たりました");

            if (gameObject.TryGetComponent(out Rigidbody rb))
            {
                rb.AddForce(((other.transform.position - transform.position).normalized + Vector3.up).normalized
                            * _cmpPlayerOnCollision.RegainKnockbackForce,
                    ForceMode.Impulse);
            }
            else
            {
                rb = gameObject.AddComponent<Rigidbody>();
                rb.AddForce(((other.transform.position - transform.position).normalized + Vector3.up).normalized
                            * _cmpPlayerOnCollision.RegainKnockbackForce,
                    ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            // スピードを止める
            _cmp.CurrentMaxSpeed = _cmpPlayerOnCollision.RegainSpeed;
            Debug.Log("障害物に当たりました");
        }
    }
}