using System;
using System.Collections;
using System.Collections.Generic;
using ImTipsyDude.IniD.Player;
using ImTipsyDude.InstantECS;
using UnityEngine;
using DG.Tweening;
using ImTipsyDude.Helper;

public class SysCoin : IECSSystem
{
    private CmpCoin _cmpCoin;
    private Rigidbody _rigidbody;
    private Collider _collider;

    private bool _isGotten = false;

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
            var currentScene = GetEntity<EnCoin>().World.CurrentScene;

            currentScene
                .PullSystem(EnInstanceIdPool.Instance.Map[nameof(SysScoreView)], out SysScoreView s);
            s.AddScore(_cmpCoin.Score);

            // ここでパーティクルを出す
            currentScene.PullSystem(EnInstanceIdPool.Instance.Map[nameof(SysPlayerShuriken)],
                out SysPlayerShuriken shuriken);
            shuriken.PlayBuff();

            // UIのカウントを増やす
            if (!_isGotten)
            {
                currentScene.PullSystem(EnInstanceIdPool.Instance.Map[nameof(SysGotCoinView)],
                    out SysGotCoinView coinView);
                coinView.GotCoin();
                _isGotten = true;
            }

            Debug.Log("打ち上げた");

            //スピードを上げる値をどこから取得するか悩んでるから一旦仮置き
            if (_cmpCoin.WillSpeedUp)
                player.SpeedUp(_cmpCoin.IncreaseInSpeed);

            //取得したら打ち上げる
            _rigidbody.AddForce(Vector3.up * _cmpCoin.NockUpSpeed, ForceMode.Impulse);

            var colliders = GetComponents<Collider>();
            foreach (var c in colliders)
            {
                c.isTrigger = true;
            }

            transform.DORotate(new Vector3(0, 360 * _cmpCoin.RotateTimesPerSec * _cmpCoin.DestoroyDuration, 0),
                _cmpCoin.DestoroyDuration, RotateMode.FastBeyond360).OnComplete(() => Destroy(this.gameObject));
        }
    }
}