using System;
using System.Collections;
using System.Collections.Generic;
using ImTipsyDude.IniD.Player;
using ImTipsyDude.InstantECS;
using UnityEngine;
using DG.Tweening;

public class SysCoin : IECSSystem
{
    private CmpCoin _cmpCoin;

    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    public override void OnStart()
    {
        _cmpCoin = GetComponent<CmpCoin>();
        if (!gameObject.TryGetComponent(out _rigidbody))
        {
            _rigidbody = gameObject.AddComponent<Rigidbody>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<SysIniDPlayer>(out var player))
        {
            Debug.Log("打ち上げた");
            //スピードを上げる値をどこから取得するか悩んでるから一旦仮置き
            player.SpeedUp(_cmpCoin.IncreaseInSpeed);
            //取得したら打ち上げる
            _rigidbody.AddForce(Vector3.up * _cmpCoin.NockUpSpeed, ForceMode.Impulse);
            transform.DOLocalRotate(new Vector3(0, 360 * 3, 0), 1).OnComplete(() => Destroy(this.gameObject));
        }
    }
}