using System;
using ImTipsyDude.InstantECS;
using UnityEngine;

public class SysCheckPoint : IECSSystem
{
    private EnCheckPoint _checkPoint;

    public override void OnStart()
    {
        _checkPoint = GetEntity<EnCheckPoint>();
    }

    private void Do()
    {
        Instantiate(_checkPoint.Prefab, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        Do();
    }
}