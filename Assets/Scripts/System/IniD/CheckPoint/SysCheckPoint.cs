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

    private void RandomInstantiate()
    {
        for (int i = 0; i < 10; i++)
        {
            var offset = UnityEngine.Random.insideUnitSphere;
            Instantiate(_checkPoint.Prefab,
                transform.position + offset,
                Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RandomInstantiate();
        }
    }
}